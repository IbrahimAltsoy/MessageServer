using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Mailing;
using Domain.Entities;
using MediatR;
using MimeKit;

namespace Application.Features.Auth.Commands.EmailActivationCodeReSend;

public class EmailActivationCodeReSendCommand : IRequest
{
    public Guid UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public class EmailActivationCodeReSendCommandHandler : IRequestHandler<EmailActivationCodeReSendCommand>
    {
        private readonly IUserRepository _userRepository;
        //private readonly IAuthenticatorService _authenticatorService;
        private readonly IMailService _mailService;
        private readonly AuthBusinessRules _authBusinessRules;

        public EmailActivationCodeReSendCommandHandler(
            IUserRepository userRepository, 
            //IAuthenticatorService authenticatorService, 
            IMailService mailService, 
            AuthBusinessRules authBusinessRules
            )
        {
            _userRepository = userRepository;
            //_authenticatorService = authenticatorService;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(EmailActivationCodeReSendCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(m=>m.Id == request.UserId);

            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserEmailVerifyCheck(user);

            //user.EmailActivationKey = await _authenticatorService.CreateEmailVerify();

            await _userRepository.UpdateAsync(user);
            var toEmailList = new List<MailboxAddress> { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            _mailService.SendMail(
                new Mail
                {
                    ToList = toEmailList,
                    Subject = "Verify Your Email - WiaCard",
                    TextBody =
                        $"Click on the link to verify your email: {request.VerifyEmailUrlPrefix}?ActivationKey= 'HttpUtility.UrlEncode(user.EmailActivationKey)'"
                }
            );
        }
    }
}
