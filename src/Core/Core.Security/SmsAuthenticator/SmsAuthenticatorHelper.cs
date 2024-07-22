using System.Security.Cryptography;

namespace Core.Security.SmsAuthenticator;

public class SmsAuthenticatorHelper : ISmsAuthenticatorHelper
{
    public Task<string> CreateSmsActivationCode()
    {
        string code = RandomNumberGenerator
            .GetInt32(Convert.ToInt32(Math.Pow(x: 10, y: 6)))
            .ToString()
            .PadLeft(totalWidth: 6, paddingChar: '0');
        return Task.FromResult(code);
    }

    public Task<string> CreateSmsActivationKey()
    {
        string code = RandomNumberGenerator
            .GetInt32(Convert.ToInt32(Math.Pow(x: 10, y: 6)))
            .ToString()
            .PadLeft(totalWidth: 6, paddingChar: '0');
        return Task.FromResult(code);
    }
}
