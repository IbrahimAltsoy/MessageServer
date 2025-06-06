﻿using MediatR;

namespace Application.Features.Smses.Commands.CreatedSmsDelivery
{
    public class CreatedSmsDeliveryCommandRequest:IRequest<CreatedSmsDeliveryCommandResponse>
    {
        // yüklenen sms ,
        // müşteri id geliyor,
        // müşteri namesurmane geliyor,
        // müşteri productName geliyor,
        // gelen bu veriler doğrultusunda yeni sms kaydı oluşturuluyor
       
     
        public Guid CustomerId { get; set; }
        public string NameSurname { get; set; }
        public string ProductName { get; set; }
        public string Phone {  get; set; }

    }
}
