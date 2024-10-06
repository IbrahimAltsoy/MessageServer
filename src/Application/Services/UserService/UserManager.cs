using Application.Features.Panel.Queries.UserStateUsersIstatic;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Enums;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;


namespace Application.Services.UserService;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> GenerateQrCodeAsync(string formUrl)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(formUrl, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20);
        return Convert.ToBase64String(qrCodeBytes); // Base64 string olarak döndür
    }

    public async Task<User?> GetByEmail(string email)
    {
        User? user = await _userRepository.GetAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> GetById(Guid id)
    {
        User? user = await _userRepository.GetAsync(u => u.Id == id);
        return user;
    }

    public async Task<User> GetByPhone(string phone)
    {
        User? user = await _userRepository.GetAsync(u=>u.Phone== phone);
        return user;
    }

    public async Task<User> Update(User user)
    {
        User updatedUser = await _userRepository.UpdateAsync(user);
        return updatedUser;
    }

    public async Task<UserStateUsersIstaticQueryResponse> UserStateUsersIstaticQueryResponseAsync()
    {
        var totalUsers = await _userRepository.GetListAsync();
        var activeUsers = await _userRepository.GetListAsync(x => x.UserStatus == UserStatus.Active);
        var passiveUsers = await _userRepository.GetListAsync(x => x.UserStatus == UserStatus.Passive);
        var inactiveUsers = await _userRepository.GetListAsync(x => x.UserStatus == UserStatus.Inactive);
        var blockedUsers = await _userRepository.GetListAsync(x => x.UserStatus == UserStatus.Blocked);
        var deletedUsers = await _userRepository.GetListAsync(x => x.UserStatus == UserStatus.Deleted);
        var totalCount = totalUsers.Count();

        var model = new UserStateUsersIstaticQueryResponse
        {
            TotalUsers = totalCount,
            ActiveUsers = activeUsers.Count(),
            PassiveUsers = passiveUsers.Count(),
            InactiveUsers = inactiveUsers.Count(),
            BlockedUsers = blockedUsers.Count(),
            DeletedUsers = deletedUsers.Count(),
            ActiveUserPercentage = CalculatePercentage(activeUsers.Count(), totalCount),
            PassiveUserPercentage = CalculatePercentage(passiveUsers.Count(), totalCount),
            InactiveUserPercentage = CalculatePercentage(inactiveUsers.Count(), totalCount),
            BlockedUserPercentage = CalculatePercentage(blockedUsers.Count(), totalCount),
            DeletedUserPercentage = CalculatePercentage(deletedUsers.Count(), totalCount)
        };
        return model;
    }
    private double CalculatePercentage(int count, int total)
    {
        return total > 0 ? (double)count / total * 100 : 0;
    }
}
