using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.PasswordReset;

public class PasswordResetCommand : IRequest
{
    public string NewPassword { get; set; }
    public string PasswordResetToken { get; set; }

    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public PasswordResetCommandHandler(IUserRepository userRepository, IPasswordResetTokenRepository passwordResetTokenRepository, AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            PasswordResetToken findedToken = await _passwordResetTokenRepository.GetAsync(p => p.Token == request.PasswordResetToken);
            // TODO: nullCheck, expiresCheck
            User findedUser = await _userRepository.GetAsync(u => u.Email == findedToken.Email);
            // TODO: nullCheck

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);
            findedUser.PasswordSalt = passwordSalt;
            findedUser.PasswordHash = passwordHash;

            await _userRepository.UpdateAsync(findedUser);
            await _passwordResetTokenRepository.DeleteAsync(findedToken, true);
        }
    }
}
