namespace Core.Security.SmsAuthenticator;

public interface ISmsAuthenticatorHelper
{
    public Task<string> CreateSmsActivationKey();
    public Task<string> CreateSmsActivationCode();
}
