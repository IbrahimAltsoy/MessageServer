using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.MailSenderService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using MimeKit;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator;

public class EnableEmailAuthenticatorCommand : IRequest
{
    public Guid UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        //private readonly IAuthenticatorService _authenticatorService;
        //private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
       // private readonly IMailSenderService _mailSenderService;
        private readonly IUserService _userService;

        public EnableEmailAuthenticatorCommandHandler(
            IUserService userService,
            //IEmailAuthenticatorRepository emailAuthenticatorRepository,
            AuthBusinessRules authBusinessRules
            //IMailSenderService mailSenderService,
            //IAuthenticatorService authenticatorService
        )
        {
            _userService = userService;
            //_emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authBusinessRules = authBusinessRules;
            //_mailSenderService = mailSenderService;
           // _authenticatorService = authenticatorService;
        }

        public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserShouldNotBeHaveAuthenticator(user);

            user.AuthenticatorType = AuthenticatorType.Email;
            await _userService.Update(user);

            //EmailAuthenticator emailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);
            //EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);

            //await _mailSenderService.EmailEnableAuthenticator(user, addedEmailAuthenticator.ActivationKey);
        }
    }
}
