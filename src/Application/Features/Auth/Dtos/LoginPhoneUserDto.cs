using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Dtos
{
    public class LoginPhoneUserDto
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }


        public LoginPhoneUserDto(Guid id, string phone, string name, string companyName)
        {
            Id = id;
            Phone = phone;
            Name = name;
            CompanyName = companyName;
        }
    }
}
