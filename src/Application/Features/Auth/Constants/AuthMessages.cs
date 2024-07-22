namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string EmailAuthenticatorDontExists = "Eposta doðrulayýcýsý mevcut deðil.";
    public const string OtpAuthenticatorDontExists = "Otp doðrulayýcýsý mevcut deðil.";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Zaten doðrulanmýþ otp kimlik doðrulayýcýsý var.";
    public const string EmailActivationKeyDontExists = "Eposta aktivasyon kodunuz bulunamadý.";
    public const string UserDontExists = "Kullanýcý bilgilerinizi kontrol ederek tekrar deneyin.";
    public const string UserHaveAlreadyAAuthenticator = "Zaten bir kimlik doðrulayýcýnýz var.";
    public const string RefreshDontExists = "Yenileme kodu bulunamadý.";
    public const string InvalidRefreshToken = "Hatalý yenileme kodu.";
    public const string UserMailAlreadyExists = "Kullanýcý mevcut.";
    public const string PasswordDontMatch = "Giriþ bilgilerinizi kontrol edin.";
    public const string EmailVerifyActivationKeyDontExists = "Eposta doðrulama kodu mevcut deðil.";
    public const string UserEmailHasAlreadyBeenVerified = "Eposta adresiniz zaten doðrulanmýþ.";
    public const string UserPhoneDontExist = "Telefon numaranýzý girin.";
    public const string UserPhoneHasAlreadyBeenVerified = "Telefon numaranýz zaten doðrulanmýþ.";
    public const string UserSmsRequestTimedOut = "Doðrulama süresi geçti.";
    public const string UserSmsActivationKeyControl = "Sms aktivasyonu doðrulanamadý!";
    public const string InvalidPhoneVerificationToken = "Hatalý doðrulama kodu!";
    public const string NewEmailValidationTokenSend = "Mail doðrulama kodunuzun süresi geçti. Yeni kodunuz mailinize gönderildi.";
    public const string UserEmailHasNotBeenVerified = "Eposta adresiniz doðrulanmamýþ. Lütfen epostanýza gelen baðlantýdan doðrulama yapýn.";
}