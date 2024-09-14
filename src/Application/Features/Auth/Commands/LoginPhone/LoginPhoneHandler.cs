using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.JWT;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.LoginPhone
{
    public class LoginPhoneHandler : IRequestHandler<LoginPhoneRequest, LoginPhoneResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public LoginPhoneHandler(AuthBusinessRules authBusinessRules, IAuthenticatorService authenticatorService, IAuthService authService, IUserService userService)
        {
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _authService = authService;
            _userService = userService;
        }

        public async Task<LoginPhoneResponse> Handle(LoginPhoneRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetByPhone(request.Phone);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user, request.Password);
            //await _authBusinessRules.UserEmailVerifyCheck(user);
            await _authBusinessRules.UserShouldNotBeHavePhone(user);
            LoginPhoneResponse loginPhoneResponse = new ();
            if (user.AuthenticatorType is not AuthenticatorType.None)
            {
                if (request.AuthenticatorCode is null)
                {
                    await _authenticatorService.SendAuthenticatorCode(user);
                    loginPhoneResponse.RequiredAuthenticatorType = user.AuthenticatorType;
                    throw new BusinessException("2FA");
                    //return loggedResponse;
                }

                await _authenticatorService.VerifyAuthenticatorCode(user, request.AuthenticatorCode);
            }
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
