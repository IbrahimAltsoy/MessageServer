using Application.Services.MailSenderService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.OtpAuthenticator;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.AuthenticatorService;

public class AuthenticatorManager : IAuthenticatorService
{
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
    private readonly IMailSenderService _mailSenderService;

    public AuthenticatorManager(
        IEmailAuthenticatorHelper emailAuthenticatorHelper,
        IEmailAuthenticatorRepository emailAuthenticatorRepository,
        IMailSenderService mailSenderService,
        IOtpAuthenticatorHelper otpAuthenticatorHelper
    )
    {
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _mailSenderService = mailSenderService;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
    }

    public async Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        string result = await _otpAuthenticatorHelper.ConvertSecretKeyToString(secretKey);
        return result;
    }

    public async Task SendAuthenticatorCode(User user)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await _sendAuthenticatorCodeWithEmail(user);
    }

    public async Task VerifyAuthenticatorCode(User user, string authenticatorCode)
    {
        if (user.AuthenticatorType is AuthenticatorType.Email)
            await VerifyAuthenticatorCodeWithEmail(user, authenticatorCode);
        else if (user.AuthenticatorType is AuthenticatorType.Otp)
            await VerifyAuthenticatorCodeWithOtp(user, authenticatorCode);
    }

    private async Task _sendAuthenticatorCodeWithEmail(User user)
    {
        var data = await _emailAuthenticatorRepository.GetListAsync(c => c.UserId == user.Id);
        await _emailAuthenticatorRepository.DeleteRangeAsync(data, true);
        string authenticatorCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();
        EmailAuthenticator emailAuthenticator = new EmailAuthenticator
        {
            UserId = user.Id,
            ActivationKey = authenticatorCode,
            Expires = DateTime.UtcNow.AddMinutes(5),
        };

        await _emailAuthenticatorRepository.AddAsync(emailAuthenticator);
        await _mailSenderService.EmailAuthenticator(user, authenticatorCode);
    }

    private async Task VerifyAuthenticatorCodeWithEmail(User user, string authenticatorCode)
    {
        EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);
        if (emailAuthenticator is null)
            throw new NotFoundException("Email Authenticator not found.");
        if (emailAuthenticator.ActivationKey != authenticatorCode)
            throw new BusinessException("Authenticator code is invalid.");

        await _emailAuthenticatorRepository.DeleteAsync(emailAuthenticator, true);
    }

    private async Task VerifyAuthenticatorCodeWithOtp(User user, string authenticatorCode)
    {
        //OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(e => e.UserId == user.Id);
        //if (otpAuthenticator is null)
        //    throw new NotFoundException("Otp Authenticator not found.");
        //bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, authenticatorCode);
        //if (!result)
        //    throw new BusinessException("Authenticator code is invalid.");
    }

    public async Task<string> CreateEmailVerify()
    {
        return await _emailAuthenticatorHelper.CreateEmailActivationKey(); ;
    }

    public async Task<string> CreatePhoneVerify()
    {
        byte[] code = await _otpAuthenticatorHelper.GenerateSecretKey();
        string result = await _otpAuthenticatorHelper.ConvertSecretKeyToString(code);
        return result;
    }
}
