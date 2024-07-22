using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ContactForm : Entity<Guid>
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Mail { get; set; }
    public string Description { get; set; }
    public bool Read { get; set; } = false;

    public ContactForm()
    {
        FullName = string.Empty;
        Phone = string.Empty;
        Mail = string.Empty;
        Description = string.Empty;
        Read = false;
    }

    public ContactForm(string fullName, string phone, string mail, string description, bool read)
    {
        FullName = fullName;
        Phone = phone;
        Mail = mail;
        Description = description;
        Read = read;
    }

    public ContactForm(Guid id, string fullName, string phone, string mail, string description, bool read) : base(id)
    {
        FullName = fullName;
        Phone = phone;
        Mail = mail;
        Description = description;
        Read = read;
    }
}