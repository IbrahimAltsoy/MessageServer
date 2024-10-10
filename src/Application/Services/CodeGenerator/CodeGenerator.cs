using Core.Security.OtpAuthenticator;
using System.Security.Cryptography;

namespace Application.Services.CodeGenerator;

public class CodeGenerator : ICodeGenerator
{
    public async Task<string> OtpCodeGenerator()
    {
        string code = RandomNumberGenerator
            .GetInt32(Convert.ToInt32(Math.Pow(x: 10, y: 6)))
            .ToString()
            .PadLeft(totalWidth: 6, paddingChar: '0');
        return code;
    }

   
}
