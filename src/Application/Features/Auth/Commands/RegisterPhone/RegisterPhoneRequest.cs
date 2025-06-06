﻿using MediatR;

namespace Application.Features.Auth.Commands.PhoneRegister
{
    public class RegisterPhoneRequest:IRequest<RegisterPhoneResponse>
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
