using Core.Security.JWT;
using Domain.Entities;

namespace Application.Features.Auth.Dtos;

public class TokensDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }

    public TokensDto(AccessToken accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
