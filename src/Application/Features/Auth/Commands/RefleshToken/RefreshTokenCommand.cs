using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Domain.Entities;
using Core.Security.JWT;
using MediatR;
using Application.Features.Auth.Commands.Login;

namespace Application.Features.Auth.Commands.RefleshToken;

public class RefreshTokenCommand : IRequest<RefreshedTokensResponse>
{
    public string? RefleshToken { get; set; }
    public string? IPAddress { get; set; }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshedTokensResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RefreshTokenCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RefreshedTokensResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            RefreshToken? refreshToken = await _authService.GetRefreshTokenByToken(request.RefleshToken);
            await _authBusinessRules.RefreshTokenShouldBeExists(refreshToken);

            if (refreshToken.Revoked != null)
                await _authService.RevokeDescendantRefreshTokens(
                    refreshToken,
                    request.IPAddress,
                    reason: $"Attempted reuse of revoked ancestor token: {refreshToken.Token}"
                );
            await _authBusinessRules.RefreshTokenShouldBeActive(refreshToken);

            User user = await _userService.GetById(refreshToken.UserId);

            RefreshToken newRefreshToken = await _authService.RotateRefreshToken(user, refreshToken, request.IPAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(newRefreshToken);

            await _authService.DeleteOldRefreshTokens(refreshToken.UserId);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(user);
            createdAccessToken.RefreshToken = addedRefreshToken.Token;

            RefreshedTokensResponse refreshedTokensResponse = new();
            refreshedTokensResponse.AccessToken = createdAccessToken;
            refreshedTokensResponse.AccessToken.RefreshToken = addedRefreshToken.Token;
            refreshedTokensResponse.User = new Dtos.LoginUserDto(user.Id, user.Email, $"{user.FirstName} {user.LastName}");
            return refreshedTokensResponse;
        }
    }
}
