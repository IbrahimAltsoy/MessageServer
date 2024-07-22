namespace Core.Security.JWT;

public class AccessToken
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }

    public AccessToken()
    {
        Token = string.Empty;
        RefreshToken = string.Empty;
    }

    public AccessToken(string token, string refreshToken, DateTime expiration)
    {
        Token = token;
        RefreshToken = refreshToken;
        Expiration = expiration;
    }
}
