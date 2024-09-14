using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Services.MailSenderService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Core.Security.EmailAuthenticator;
using Core.Security.Hashing;
using Core.Security.SmsAuthenticator;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.PhoneRegister
{
    public class PhoneRegisterHandler : IRequestHandler<PhoneRegisterRequest, PhoneRegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMailSenderService _mailSenderService;
        private readonly ISmsAuthenticatorHelper _smsAuthenticatorHelper;
        private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IPhoneVerificationTokenRepository _phoneVerificationTokenRepository;
        private readonly IUserService _userService;

        public PhoneRegisterHandler(
            IUserRepository userRepository, 
            AuthBusinessRules authBusinessRules, 
            IMailSenderService mailSenderService, 
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            ISmsAuthenticatorHelper smsAuthenticatorHelper, 
            IEmailVerificationTokenRepository emailVerificationTokenRepository, 
            IPhoneVerificationTokenRepository phoneVerificationTokenRepository, 
            IUserService userService)
        {
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
            _mailSenderService = mailSenderService;
            _emailAuthenticatorHelper = emailAuthenticatorHelper;
            _smsAuthenticatorHelper = smsAuthenticatorHelper;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _phoneVerificationTokenRepository = phoneVerificationTokenRepository;
            _userService = userService;
        }

        public async Task<PhoneRegisterResponse> Handle(PhoneRegisterRequest request, CancellationToken cancellationToken)
        {
            //await _authBusinessRules.UserEmailShouldBeNotExists(request.Email);
            // buraya bir BusinessRole ekle telefon numarası olup olmadığını kontrol ettir.

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            // Kullanıcıyı geçici QR kodu ile oluştur
            User newUser = new()
            {
                Phone = request.Phone,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CompanyName = request.CompanyName,
                QRCode = null, // Geçici QR kodu veya null
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserStatus = UserStatus.Passive,
            };
            User createdUser = await _userRepository.AddAsync(newUser);
            var userId = newUser.Id;
            var qrCodeUrl = $"http://localhost:7030/Customer/Form/{userId}";
            var qrCode = await _userService.GenerateQrCodeAsync(qrCodeUrl);
            createdUser.QRCode = qrCode;

            await _userRepository.UpdateAsync(createdUser);

            string verificationToken = await _smsAuthenticatorHelper.CreateSmsActivationKey();

            PhoneVerificationToken token = new()
            {
                UserId = userId,
                Expires = DateTime.UtcNow.AddDays(7),
                Token = verificationToken


            };
            PhoneVerificationToken phoneVerificationToken = await _phoneVerificationTokenRepository.AddAsync(token);

            //await _mailSenderService.NewUserMail(createdUser, createdToken);
            return new PhoneRegisterResponse()
            {
                Code = verificationToken
            };
            

        }
    }
}
