using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.UserService;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.JWT;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.LoginPhone
{
    public class LoginPhonePasswordHandler : IRequestHandler<LoginPhonePasswordRequest, LoginPhonePasswordResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ISmsService _smsService;
        private readonly IPhoneVerificationTokenRepository _phoneVerificationTokenRepository;

        public LoginPhonePasswordHandler(AuthBusinessRules authBusinessRules, IAuthenticatorService authenticatorService, IAuthService authService, IUserService userService, ISmsService smsService, IPhoneVerificationTokenRepository phoneVerificationTokenRepository)
        {
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _phoneVerificationTokenRepository = phoneVerificationTokenRepository;
            _authService = authService;
            _userService = userService;
            _smsService = smsService;
        }

        public async Task<LoginPhonePasswordResponse> Handle(LoginPhonePasswordRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByPhone(request.Phone);
            await _authBusinessRules.UserShouldBeExists(user);
            
            LoginPhonePasswordResponse loginPhoneResponse = new();

            await _authBusinessRules.UserPasswordShouldBeMatch(user, request.Password);

            //string phoneNumber = user.Phone;
            //if (phoneNumber.StartsWith("0"))
            //{
            //    phoneNumber = phoneNumber.Substring(1);
            //}
            //if (request.AuthenticatorCode is null)
            //{
            //    throw new BusinessException("Kod gönderildi. Lütfen doğrulama kodunu girin.");
            //}

        

            
            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
            RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
            await _authService.DeleteOldRefreshTokens(user.Id);

            loginPhoneResponse.AccessToken = createdAccessToken;
            loginPhoneResponse.AccessToken.RefreshToken = addedRefreshToken.Token;
            loginPhoneResponse.User = new LoginPhoneUserDto(user.Id, user.Phone, user.FirstName, user.CompanyName);

            return loginPhoneResponse;
        }
    }
}
