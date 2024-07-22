using Domain.Entities;

namespace Application.Services.AuthenticatorService;

public interface IAuthenticatorService
{
    public Task<string> CreateEmailVerify();
    public Task<string> CreatePhoneVerify();
    public Task<string> ConvertSecretKeyToString(byte[] secretKey);
    public Task SendAuthenticatorCode(User user);
    public Task VerifyAuthenticatorCode(User user, string authenticatorCode);
}
