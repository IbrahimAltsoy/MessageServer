﻿using Application.Features.Customers.Queries.CustomerGetAllByUser;
using Application.Services.Repositories;
using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Persistence.Configurations;
using SmartVisitServer.Web.Models;
using SmartVisitServer.Web.Services.Customers;
using System.Drawing.Printing;

namespace SmartVisitServer.Web.Controllers
{
   
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "http://localhost:5011/api/Customers";
        readonly ICustomerService _customerService;
        private readonly IUserRepository _userRepository;
        

        public CustomerController(IHttpClientFactory httpClientFactory, ICustomerService customerService, IUserRepository userRepository)
        {
            _httpClientFactory = httpClientFactory;
            _customerService = customerService;
            _userRepository = userRepository;
           
        }

        //[HttpGet]
        //public async Task<IActionResult> CustomerGetAllByUser(int page=0, int pageSize=8, TimePeriodType timePeriod = TimePeriodType.Yearly)
        //{
        //    var pagedList = await _customerService.GetCustomersAsync(page, pageSize, timePeriod);
        //    return View(pagedList);
        //}
        [HttpGet]
        public async Task<IActionResult> ListUsersWithQrCodes()
        {

            var users = await _userRepository.GetListAsync();
            List<UserQrCode> userQrCodeList = users.Select(user => new UserQrCode
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                QRCode = user.QRCode
            }).ToList();

            return View(userQrCodeList);
        }
        [HttpGet]
        public async Task<IActionResult> CustomerGetAllByUser(int page = 0, int pageSize = 8, TimePeriodType? timePeriod = TimePeriodType.Yearly, string? searchTerm=null)
        {           
            if (string.IsNullOrEmpty(searchTerm))
            {
                //ViewBag.SelectedTimePeriod = timePeriod ?? TimePeriodType.Yearly;
                var pagedList = await _customerService.GetCustomersAsync(page, pageSize, timePeriod);
                ViewBag.SearchTerm = string.Empty;
                ViewBag.CurrentPage = page;               
                ViewBag.TimePeriod = timePeriod;
                return View(pagedList);
            }
            else
            {
                var pagedList = await _customerService.CustomerSearchByPhoneOrNameSurnameAsync(searchTerm);
                ViewBag.SearchTerm = searchTerm;
                ViewBag.CurrentPage = page;              
                ViewBag.TimePeriod ="";
                return View(pagedList);
            }
           
        }
       
            

            //    return View(model);
            //}

            //[HttpGet]
            //public async Task<IActionResult> Form(Guid id)
            //{
            //    // Id'ye göre veriyi alın (örneğin, kullanıcı bilgilerini)
            //    Domain.Entities.User? user =await _userRepository.GetAsync(x=>x.Id==id);

            //    if (user == null)
            //    {
            //        return NotFound();
            //    }
            //    CustomerModel customer = new CustomerModel()
            //    {
            //        Id = user.Id,
            //    };
            //    return View(customer); // Model ile formu döndürün
            //}
            //[HttpPost("form/submit")]
            //public async Task<IActionResult> SubmitForm([FromForm] CustomerModel model)
            //{
            //    // Id'yi ve diğer form verilerini işleyin
            //    var user = await _userRepository.GetAsync(x=>x.Id==model.Id);

            //    if (user == null)
            //    {
            //        return NotFound();
            //    }

            //    Customer customer = new Customer()
            //    {
            //        NameSurname = model.NameSurname,
            //        Phone = model.Phone,
            //        ProductName = model.ProductName,
            //        Description = model.Description,
            //        UserId = user.Id,
            //    };
            //    await _customerRepository.AddAsync(customer);
            //    return RedirectToAction("Home"); // Başarı sayfasına yönlendir
            //}

        }
}
