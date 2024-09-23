using Application.Abstract.Common;
using Application.Features.Auth.Rules;
using Application.Services.MailSenderService;
using Application.Services.MembershipServices;
using Application.Services.Repositories;
using Application.Services.UserService;
using Core.Application.Dtos;
using Core.Security.EmailAuthenticator;
using Core.Security.Hashing;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest
{
    public UserForRegisterDto? UserForRegisterDto { get; set; }
    //public string IPAddress { get; set; }
    public string? VerifyEmailUrlPrefix { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>//, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMailSenderService _mailSenderService;
        private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IUserService _userService;
        private readonly IMembershipService _membershipService;
        //private readonly IOperationClaimService _operationClaimService;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IMailSenderService mailSenderService,
            AuthBusinessRules authBusinessRules,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            IUserService userService,
            IMembershipService membershipService
            )
        {
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
            _mailSenderService = mailSenderService;
            _emailAuthenticatorHelper = emailAuthenticatorHelper;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _userService = userService;
            _membershipService = membershipService;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

            // Kullanıcıyı geçici QR kodu ile oluştur
            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                QRCode = null, // Geçici QR kodu veya null
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserStatus = UserStatus.Passive,
            };

            // Kullanıcıyı veritabanına ekle
            User createdUser = await _userRepository.AddAsync(newUser);
            await _membershipService.CreateMembershipAsync(createdUser.Id);// Register olduğunda üyelik eklesin

            // Kullanıcının Id'sini al
            var userId = newUser.Id;

            // QR kodunu oluşturun ve kullanıcıyı güncelleyin
            var qrCodeUrl = $"http://localhost:7030/Customer/Form/{userId}";
            var qrCode = await _userService.GenerateQrCodeAsync(qrCodeUrl);

            createdUser.QRCode = qrCode;

            // Kullanıcıyı QR koduyla güncelle
            await _userRepository.UpdateAsync(createdUser);

            // Email aktivasyon anahtarını oluşturun
            string verificationToken = await _emailAuthenticatorHelper.CreateEmailActivationKey();

            EmailVerificationToken token = new()
            {
                Email = createdUser.Email,
                Expires = DateTime.UtcNow.AddDays(7),
                Token = verificationToken
            };

            EmailVerificationToken createdToken = await _emailVerificationTokenRepository.AddAsync(token);

            await _mailSenderService.NewUserMail(createdUser, createdToken);
        }

    }
}
