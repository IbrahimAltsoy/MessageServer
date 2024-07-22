using Application.Features.Auth.Constants;
using Application.Services.MailSenderService;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.EmailAuthenticator;
using Core.Security.Hashing;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
    private readonly IPhoneVerificationTokenRepository _phoneVerificationTokenRepository;
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IMailSenderService _mailService;

    public AuthBusinessRules(
        IUserRepository userRepository,
        IEmailVerificationTokenRepository emailVerificationTokenRepository,
        IEmailAuthenticatorHelper emailAuthenticatorHelper,
        IMailSenderService mailSenderService,
        IPhoneVerificationTokenRepository phoneVerificationTokenRepository)
    {
        _mailService = mailSenderService;
        _userRepository = userRepository;
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _emailVerificationTokenRepository = emailVerificationTokenRepository;
        _phoneVerificationTokenRepository = phoneVerificationTokenRepository;
    }

    //public Task EmailAuthenticatorShouldBeExists(EmailAuthenticator? emailAuthenticator)
    //{
    //    if (emailAuthenticator is null)
    //        throw new BusinessException(AuthMessages.EmailAuthenticatorDontExists);
    //    return Task.CompletedTask;
    //}

    //public Task OtpAuthenticatorShouldBeExists(OtpAuthenticator? otpAuthenticator)
    //{
    //    if (otpAuthenticator is null)
    //        throw new BusinessException(AuthMessages.OtpAuthenticatorDontExists);
    //    return Task.CompletedTask;
    //}

    //public Task OtpAuthenticatorThatVerifiedShouldNotBeExists(OtpAuthenticator? otpAuthenticator)
    //{
    //    if (otpAuthenticator is not null && otpAuthenticator.IsVerified)
    //        throw new BusinessException(AuthMessages.AlreadyVerifiedOtpAuthenticatorIsExists);
    //    return Task.CompletedTask;
    //}

    public Task EmailVerifyTokenShouldBeExists(EmailVerificationToken emailVerificationToken)
    {
        if (emailVerificationToken is null)
            throw new BusinessException(AuthMessages.EmailActivationKeyDontExists);
        return Task.CompletedTask;
    }
    public async Task EmailValidCheck(EmailVerificationToken emailVerificationToken, User user)
    {
        if (emailVerificationToken.Expires > DateTime.UtcNow)
            return;

        string verificationToken = await _emailAuthenticatorHelper.CreateEmailActivationKey();
        emailVerificationToken.Token = verificationToken;
        emailVerificationToken.Expires = DateTime.UtcNow.AddDays(7);
        await _emailVerificationTokenRepository.UpdateAsync(emailVerificationToken);
        await _mailService.VerifyMail(user, emailVerificationToken);
        throw new BusinessException(AuthMessages.NewEmailValidationTokenSend);
    }

    public Task UserShouldBeExists(User? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);
        return Task.CompletedTask;
    }

    public Task UserShouldNotBeHaveAuthenticator(User user)
    {
        if (user.AuthenticatorType != AuthenticatorType.None)
            throw new BusinessException(AuthMessages.UserHaveAlreadyAAuthenticator);
        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeExists(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException(AuthMessages.RefreshDontExists);
        return Task.CompletedTask;
    }

    public Task RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.Revoked != null && DateTime.UtcNow >= refreshToken.Expires)
            throw new BusinessException(AuthMessages.InvalidRefreshToken);
        return Task.CompletedTask;
    }

    public async Task UserEmailShouldBeNotExists(string email)
    {
        User? user = await _userRepository.GetAsync(predicate: u => u.Email == email, enableTracking: false);
        if (user != null)
            throw new BusinessException(AuthMessages.UserMailAlreadyExists);
    }

    public Task UserPasswordShouldBeMatch(User user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException(AuthMessages.PasswordDontMatch);

        return Task.CompletedTask;
    }

    public Task EmailVerifyActivationKeyShouldBeExists(User user)
    {

        return Task.CompletedTask;
    }

    public async Task UserEmailVerifyCheck(User? user)
    {
        if (user.EmailVerified is null)
        {
            EmailVerificationToken? vToken = await _emailVerificationTokenRepository.GetAsync(v=>v.Email == user.Email);
            if(vToken is null)
            {
                string createdToken = await _emailAuthenticatorHelper.CreateEmailActivationKey();
                EmailVerificationToken newEmailVerificationToken = new(email: user.Email, expires: DateTime.UtcNow.AddDays(7), token: createdToken);
                EmailVerificationToken createdEmailVerificationToken = await _emailVerificationTokenRepository.AddAsync(newEmailVerificationToken);
                await _mailService.VerifyMail(user, createdEmailVerificationToken);
                throw new BusinessException(AuthMessages.UserEmailHasNotBeenVerified);

            }
            if(vToken.Expires < DateTime.UtcNow)
            {
                string createdToken = await _emailAuthenticatorHelper.CreateEmailActivationKey();

                vToken.Expires = DateTime.UtcNow.AddDays(7);
                vToken.Token = createdToken;

                await _emailVerificationTokenRepository.UpdateAsync(vToken);
                await _mailService.VerifyMail(user, vToken);
                throw new BusinessException(AuthMessages.UserEmailHasNotBeenVerified);
            }
            throw new BusinessException(AuthMessages.UserEmailHasNotBeenVerified);
        }
    }

    public Task UserShouldNotBeHavePhone(User user)
    {
        if (user.Phone == "")
            throw new BusinessException(AuthMessages.UserPhoneDontExist);
        return Task.CompletedTask;
    }

    public async Task UserSmsRequestTimedOut(PhoneVerificationToken? phoneVerificationToken)
    {
        if (phoneVerificationToken.Expires < DateTime.UtcNow)
        {
            await _phoneVerificationTokenRepository.DeleteAsync(phoneVerificationToken);
            throw new BusinessException(AuthMessages.UserSmsRequestTimedOut);
        }
    }

    public Task UserSmsActivationKeyControl(PhoneVerificationToken? phoneVerificationToken, string token)
    {
        if (phoneVerificationToken.Token != token)
            throw new BusinessException(AuthMessages.InvalidPhoneVerificationToken);

        return Task.CompletedTask;
    }
}
