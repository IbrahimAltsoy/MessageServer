using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyPhone;

public class VerifyPhoneCommand : IRequest
{
    public Guid UserId { get; set; }
    public string Token { get; set; }

    public class VerifyPhoneCommandHandler : IRequestHandler<VerifyPhoneCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IUserService _userService;
        private readonly IPhoneVerificationTokenRepository _phoneVerificationTokenRepository;

        public VerifyPhoneCommandHandler(
            AuthBusinessRules authBusinessRules, 
            IAuthenticatorService authenticatorService, 
            IPhoneVerificationTokenRepository phoneVerificationTokenRepository,
            IUserService userService
            )
        {
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _phoneVerificationTokenRepository = phoneVerificationTokenRepository;
            _userService = userService;
        }

        public async Task Handle(VerifyPhoneCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserShouldNotBeHavePhone(user);

            PhoneVerificationToken? token = await _phoneVerificationTokenRepository.GetAsync(predicate: c => c.UserId == request.UserId);
            await _authBusinessRules.UserSmsRequestTimedOut(token);
            await _authBusinessRules.UserSmsActivationKeyControl(token, request.Token);
            
            user.PhoneVerified = DateTime.UtcNow;

            await _phoneVerificationTokenRepository.DeleteAsync(token);
            await _userService.Update(user);
        }
    }
}
