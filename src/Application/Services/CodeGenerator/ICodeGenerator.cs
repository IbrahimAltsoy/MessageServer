namespace Application.Services.CodeGenerator;

public interface ICodeGenerator
{
    public Task<string> OtpCodeGenerator();
    
}
