using Application.Features.Auth.Rules;
using Application.Services.MailSenderService;
using Application.Services.Repositories;
using Core.Security.EmailAuthenticator;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.PasswordReset;

public class PasswordResetRequestCommand : IRequest
{
    public string Email { get; set; }

    public class PasswordResetRequestCommandHandler : IRequestHandler<PasswordResetRequestCommand>
    {
        private readonly IMailSenderService _mailSenderService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
        private readonly AuthBusinessRules _authBusinessRules;

        public PasswordResetRequestCommandHandler(
            IMailSenderService mailSenderService,
            IUserRepository userRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            AuthBusinessRules authBusinessRules
            )
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _emailAuthenticatorHelper = emailAuthenticatorHelper;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _mailSenderService = mailSenderService;
        }
        public async Task Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == request.Email);
            //TODO user kontrol

            string resetToken = await _emailAuthenticatorHelper.CreateEmailActivationKey();
            PasswordResetToken passwordResetToken = new(email:user.Email, token: resetToken, expires: DateTime.UtcNow.AddMinutes(15));
            PasswordResetToken createdToken = await _passwordResetTokenRepository.AddAsync(passwordResetToken);
            await _mailSenderService.PasswordResetMail(user, createdToken);
        }
    }
}
