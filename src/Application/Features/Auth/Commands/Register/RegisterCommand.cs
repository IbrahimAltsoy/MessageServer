using Application.Features.Auth.Rules;
using Application.Services.MailSenderService;
using Application.Services.Repositories;
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

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IMailSenderService mailSenderService,
            AuthBusinessRules authBusinessRules,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IEmailAuthenticatorHelper emailAuthenticatorHelper
            )
        {
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
            _mailSenderService = mailSenderService;
            _emailAuthenticatorHelper = emailAuthenticatorHelper;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
            User newUser =
                new()
                {
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserStatus = UserStatus.Passive,
                };

            User createdUser = await _userRepository.AddAsync(newUser);

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
