using Application.Features.Auth.Rules;
using Application.Services.UserService;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Entities;
using Core.Security.SmsAuthenticator;
using MediatR;

namespace Application.Features.Auth.Commands.VerifyPhoneSmsSendRequest;

public class VerifyPhoneSmsSendRequestCommand : IRequest, ISecuredRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => new[] { GeneralOperationClaims.Admin, GeneralOperationClaims.User };

    public class VerifyPhoneSmsSendRequestCommandHandler : IRequestHandler<VerifyPhoneSmsSendRequestCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserService _userService;
        private readonly ISmsAuthenticatorHelper _smsHelper;

        public VerifyPhoneSmsSendRequestCommandHandler(AuthBusinessRules authBusinessRules, IUserService userService, ISmsAuthenticatorHelper smsAuthenticatorHelper)
        {
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _smsHelper = smsAuthenticatorHelper;
        }

        public async Task Handle(VerifyPhoneSmsSendRequestCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserShouldBeExists(user);
            //await _authBusinessRules.UserPhoneVerifyCheck(user);
            await _authBusinessRules.UserShouldNotBeHavePhone(user);
            //await _authBusinessRules.UserSmsRequestTimedOut(user);

            string code = await _smsHelper.CreateSmsActivationCode();
            //user.PhoneActivationKey = code;
            await _userService.Update(user);

            // sending OTP SMS 
        }
    }
}