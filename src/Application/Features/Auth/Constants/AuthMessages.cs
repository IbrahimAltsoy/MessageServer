namespace Application.Features.Auth.Constants;

public static class AuthMessages
{
    public const string EmailAuthenticatorDontExists = "Eposta do�rulay�c�s� mevcut de�il.";
    public const string OtpAuthenticatorDontExists = "Otp do�rulay�c�s� mevcut de�il.";
    public const string AlreadyVerifiedOtpAuthenticatorIsExists = "Zaten do�rulanm�� otp kimlik do�rulay�c�s� var.";
    public const string EmailActivationKeyDontExists = "Eposta aktivasyon kodunuz bulunamad�.";
    public const string UserDontExists = "Kullan�c� bilgilerinizi kontrol ederek tekrar deneyin.";
    public const string UserHaveAlreadyAAuthenticator = "Zaten bir kimlik do�rulay�c�n�z var.";
    public const string RefreshDontExists = "Yenileme kodu bulunamad�.";
    public const string InvalidRefreshToken = "Hatal� yenileme kodu.";
    public const string UserMailAlreadyExists = "Kullan�c� mevcut.";
    public const string PasswordDontMatch = "Giri� bilgilerinizi kontrol edin.";
    public const string EmailVerifyActivationKeyDontExists = "Eposta do�rulama kodu mevcut de�il.";
    public const string UserEmailHasAlreadyBeenVerified = "Eposta adresiniz zaten do�rulanm��.";
    public const string UserPhoneDontExist = "Telefon numaran�z� girin.";
    public const string UserPhoneHasAlreadyBeenVerified = "Telefon numaran�z zaten do�rulanm��.";
    public const string UserSmsRequestTimedOut = "Do�rulama s�resi ge�ti.";
    public const string UserSmsActivationKeyControl = "Sms aktivasyonu do�rulanamad�!";
    public const string InvalidPhoneVerificationToken = "Hatal� do�rulama kodu!";
    public const string NewEmailValidationTokenSend = "Mail do�rulama kodunuzun s�resi ge�ti. Yeni kodunuz mailinize g�nderildi.";
    public const string UserEmailHasNotBeenVerified = "Eposta adresiniz do�rulanmam��. L�tfen epostan�za gelen ba�lant�dan do�rulama yap�n.";
}