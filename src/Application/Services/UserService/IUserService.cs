using Domain.Entities;

namespace Application.Services.UserService;

public interface IUserService
{
    public Task<User?> GetByEmail(string email);
    public Task<User> GetById(Guid id);
    public Task<User> GetByPhone(string phone);
    public Task<User> Update(User user);
    Task<string> GenerateQrCodeAsync(string formUrl);
}
