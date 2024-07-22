using Domain.Entities;

namespace Application.Services.MailSenderService;

public interface IMailSenderService
{
    Task<bool> NewUserMail(User user, EmailVerificationToken token);
    Task<bool> VerifyMail(User user, EmailVerificationToken token);
    Task<bool> EmailEnableAuthenticator(User user, string authCode);
    Task<bool> EmailAuthenticator(User user, string authCode);
    Task<bool> LoginDetailMail(User user, string ipAddress);
    Task<bool> PasswordResetMail(User user, PasswordResetToken token);
    Task<bool> SendContactMail(string[] mailAdress, ContactForm contactForm, string ipAddress);
}