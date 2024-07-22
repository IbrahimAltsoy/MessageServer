using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyEmail;

public class VerifyEmailCommand : IRequest
{
    public string ActivationKey { get; set; }

    public class VerifyEmailCommandCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;

        public VerifyEmailCommandCommandHandler(AuthBusinessRules authBusinessRules, IUserRepository userRepository, IEmailVerificationTokenRepository emailVerificationTokenRepository)
        {
            _authBusinessRules = authBusinessRules;
            _userRepository = userRepository;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
        }

        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            EmailVerificationToken? emailVerificationToken = await _emailVerificationTokenRepository.GetAsync(c=>c.Token == request.ActivationKey);

            await _authBusinessRules.EmailVerifyTokenShouldBeExists(emailVerificationToken);
            
            User? user = await _userRepository.GetAsync(e => e.Email == emailVerificationToken.Email);

            await _authBusinessRules.EmailValidCheck(emailVerificationToken, user);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.EmailVerifyActivationKeyShouldBeExists(user);

            user.EmailVerified = DateTime.UtcNow;
            user.UserStatus = UserStatus.Active;

            await _userRepository.UpdateAsync(user);
            await _emailVerificationTokenRepository.DeleteAsync(emailVerificationToken, true);
        }
    }
}
