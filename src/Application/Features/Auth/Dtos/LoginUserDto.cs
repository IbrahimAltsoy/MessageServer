namespace Application.Features.Auth.Dtos;

public class LoginUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    public LoginUserDto(Guid id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
    }
}
