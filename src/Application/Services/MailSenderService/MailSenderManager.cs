using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Web;

namespace Application.Services.MailSenderService;

public class MailSenderManager : IMailSenderService
{
    private readonly IMailService _mailService;

    ApplicationConfigurationModel _configuration;

    public MailSenderManager(
        IMailService mailService, 
        IConfiguration configuration
        )
    {
        _mailService = mailService;
        _configuration = configuration.GetSection("WebAPIConfiguration").Get<ApplicationConfigurationModel>();
    }

    public async Task<bool> EmailEnableAuthenticator(User user, string authCode)
    {
        string activationLink = $"{_configuration.EnableEmailAuthenticator}{HttpUtility.UrlEncode(authCode)}";
        try
        {
            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };

            string mailTemplate = "wwwroot/emails/email-2F-activation.html";
            StreamReader str = new StreamReader(mailTemplate);
            string mailText = str.ReadToEnd();

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "2FA onay talebi!",
                HtmlBody = mailText.Replace("[firstName]", user.FirstName).Replace("[activationLink]", activationLink)

            });
            return true;
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message);
        }
    }

    // RegisteredMail
    public async Task<bool> NewUserMail(User user, EmailVerificationToken token)
    {
        string activationLink = $"{_configuration.RegisterEmailVerified}{HttpUtility.UrlEncode(token.Token)}";

        try
        {
            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };
            string mailTemplate = "wwwroot/emails/email-activation.html";
            StreamReader str = new StreamReader(mailTemplate);
            string mailText = str.ReadToEnd();

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "Hoşgeldiniz, kayıt işleminizi tamamlayın!",
                HtmlBody = mailText.Replace("[firstName]", user.FirstName).Replace("[activationLink]", activationLink)
            });
            return true;
        }
        catch (Exception ex)
        {
            //throw new BusinessException(ex.Message);
            Console.WriteLine($"NewUserMail hatası: KullanıcıID:{user.Id} TokenID:{token.Id}");
            return false;
        }
    }

    public async Task<bool> VerifyMail(User user, EmailVerificationToken token)
    {
        string activationLink = $"{_configuration.RegisterEmailVerified}{HttpUtility.UrlEncode(token.Token)}";

        try
        {
            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };
            string mailTemplate = "wwwroot/emails/email-activation.html";
            StreamReader str = new StreamReader(mailTemplate);
            string mailText = str.ReadToEnd();

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "Eposta adresinizi doğrulayın!",
                HtmlBody = mailText.Replace("[firstName]", user.FirstName).Replace("[activationLink]", activationLink)
            });
            return true;
        }
        catch (Exception ex)
        {
            //throw new BusinessException(ex.Message);
            Console.WriteLine($"VerifyMail hatası: KullanıcıID:{user.Id} TokenID:{token.Id}");
            return false;
        }
    }

    public async Task<bool> LoginDetailMail(User user, string ipAddress)
    {
        try
        {
            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };
            string mailTemplate = "wwwroot/emails/email-login-detail.html";
            StreamReader str = new StreamReader(mailTemplate);
            string mailText = str.ReadToEnd();

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "Logged in success!",
                HtmlBody = mailText.Replace("[firstName]", user.FirstName).Replace("[ipAddress]", ipAddress)
            });
            return true;
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message);
        }
    }

    public async Task<bool> PasswordResetMail(User user, PasswordResetToken token)
    {
        string resetLink = $"{_configuration.PasswordReset}{HttpUtility.UrlEncode(token.Token)}";

        try
        {
            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };
            string mailTemplate = "wwwroot/emails/password-reset.html";
            StreamReader str = new StreamReader(mailTemplate);
            string mailText = str.ReadToEnd();

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "Parolanızı yenileyin!",
                HtmlBody = mailText.Replace("[firstName]", user.FirstName).Replace("[resetLink]", resetLink)
            });
            return true;
        }
        catch (Exception ex)
        {
            //throw new BusinessException(ex.Message);
            Console.WriteLine($"PasswordResetMail hatası: KullanıcıID:{user.Id} TokenID:{token.Id}");
            return false;
        }
    }

    public async Task<bool> EmailAuthenticator(User user, string authCode)
    {
        try
        {
            var toEmailList = new List<MailboxAddress>
            {
                new($"{user.FirstName} {user.LastName}",user.Email)
            };
            string mailTemplate = "wwwroot/emails/email-authenticator.html";
            StreamReader str = new StreamReader(mailTemplate);
            string mailText = str.ReadToEnd();
            //string aLink = $"";

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                Subject = "Tek kullanımlık giriş kodunuz!",
                HtmlBody = mailText.Replace("[firstName]", user.FirstName).Replace("[authCode]", authCode)
            });
            return true;
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message);
        }
    }

    public async Task<bool> SendContactMail(string[] mailAdress, ContactForm contactForm, string ipAddress)
    {
        try
        {
            //var toEmailList = new List<MailboxAddress> { };
            //foreach (string mail in mailAdress)
            //{
            //    toEmailList.Add(new("", mail));
            //}

            //string contentMessage = "Yeni bir iletişim talebiniz var;";
            //string contentMessageClient = "Merhaba iletinizi aldık, en kısa sürede iletişime geçeceğiz. Mesaj içeriğiniz;";
            //string mailTemplate = "wwwroot/emails/contact-form.html";
            //StreamReader str = new StreamReader(mailTemplate);
            //string mailText = str.ReadToEnd();

            //await _mailService.SendEmailAsync(new Mail
            //{
            //    ToList = toEmailList,
            //    Subject = "Yeni bir iletişim isteği aldınız!",
            //    HtmlBody = mailText
            //                    .Replace("[mailContentMessage]", contentMessage)
            //                    .Replace("[firstName]", contactForm.FirstName)
            //                    .Replace("[lastName]", contactForm.LastName)
            //                    .Replace("[eMailAddress]", contactForm.EMail)
            //                    .Replace("[phoneNumber]", contactForm.Phone)
            //                    .Replace("[formMessage]", contactForm.Topic)
            //                    .Replace("[ipAddress]", ipAddress)
            //});

            //await _mailService.SendEmailAsync(new Mail
            //{
            //    ToList = new List<MailboxAddress> { new($"{contactForm.FirstName} {contactForm.LastName}", contactForm.EMail) },
            //    Subject = "Mesajınız alındı!",
            //    HtmlBody = mailText
            //                    .Replace("[mailContentMessage]", contentMessageClient)
            //                    .Replace("[firstName]", contactForm.FirstName)
            //                    .Replace("[lastName]", contactForm.LastName)
            //                    .Replace("[eMailAddress]", contactForm.EMail)
            //                    .Replace("[phoneNumber]", contactForm.Phone)
            //                    .Replace("[formMessage]", contactForm.Topic)
            //                    .Replace("[ipAddress]", ipAddress)
            //});


            return true;
        }
        catch (Exception exp)
        {
            throw new BusinessException(exp.Message);
        }
    }

}