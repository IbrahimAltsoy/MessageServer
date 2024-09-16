using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.MailSenderService;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.UserService;
using Core.Security.EmailAuthenticator;
using Core.Security.SmsAuthenticator;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.CreatePhoneCode
{
    public class CreatePhoneCodeHandler : IRequestHandler<CreatePhoneCodeRequest, CreatePhoneCodeResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IUserService _userService;
        private readonly ISmsService _smsService;
        private readonly IPhoneVerificationTokenRepository _phoneVerificationTokenRepository;

        public CreatePhoneCodeHandler(AuthBusinessRules authBusinessRules, IAuthenticatorService authenticatorService, IUserService userService, ISmsService smsService, IPhoneVerificationTokenRepository phoneVerificationTokenRepository)
        {
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _userService = userService;
            _smsService = smsService;
            _phoneVerificationTokenRepository = phoneVerificationTokenRepository;
        }

        public async Task<CreatePhoneCodeResponse> Handle(CreatePhoneCodeRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByPhone(request.Phone);
            await _authBusinessRules.UserShouldBeExists(user);
            PhoneVerificationToken? phoneVerification = await _phoneVerificationTokenRepository.GetAsync(x => x.UserId == user.Id);
            string code = await _authenticatorService.VerifyPhoneAuthenticatorCode(phoneVerification.UserId);
            phoneVerification.Token = code;
            await _phoneVerificationTokenRepository.UpdateAsync(phoneVerification);
            string phoneNumber = user.Phone;
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = phoneNumber.Substring(1);
            }
            await _smsService.SendSms(phoneNumber,$"Lütfen kodu kimse ile paylaşmayınız! Kodunuz: {code}");
            return new CreatePhoneCodeResponse()
            {
                AuthenticatorCode = code
            };
        }
    }
}
