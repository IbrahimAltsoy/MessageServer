using Application.Features.Auth.Commands.LoginPhone;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.SmsService;
using Application.Services.UserService;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RegisterPhoneCode
{
    public class LoginPhoneCodeHandler : IRequestHandler<LoginPhoneCodeRequest, LoginPhoneCodeResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
       
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
       
        private readonly IPhoneVerificationTokenRepository _phoneVerificationTokenRepository;

        public LoginPhoneCodeHandler(AuthBusinessRules authBusinessRules, IAuthService authService, IUserService userService, IPhoneVerificationTokenRepository phoneVerificationTokenRepository)
        {
            _authBusinessRules = authBusinessRules;
            _authService = authService;
            _userService = userService;
            _phoneVerificationTokenRepository = phoneVerificationTokenRepository;
        }

        public async Task<LoginPhoneCodeResponse> Handle(LoginPhoneCodeRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByPhone(request.Phone);
            await _authBusinessRules.UserShouldBeExists(user);

            LoginPhoneCodeResponse loginPhoneResponse = new();
            PhoneVerificationToken? phoneVerification = await _phoneVerificationTokenRepository.GetAsync(x => x.UserId == user.Id);          
            if (phoneVerification.Token == request.AuthenticatorCode)
            {
                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);
                await _authService.DeleteOldRefreshTokens(user.Id);

                loginPhoneResponse.AccessToken = createdAccessToken;
                loginPhoneResponse.AccessToken.RefreshToken = addedRefreshToken.Token;
                loginPhoneResponse.User = new LoginPhoneUserDto(user.Id, user.Phone, user.FirstName, user.CompanyName);

                return loginPhoneResponse;
            }
            else
            {
                throw new Exception("Girilen kod hatalı, lütfen tekrar kod isteyiniz!");
            }


        }
    }
}
