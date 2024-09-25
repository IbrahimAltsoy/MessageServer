using Application.Services.OperationClaimService;
using Application.Services.Repositories;
using Application.Services.SmsSettings;
using Application.Services.UserService;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        readonly BaseDbContext _dbContext;
        readonly ISmsSettingsService _smsSettingsService;
        readonly ISmsDefaultTemplateRepository _defaultTemplateRepository;
        readonly IUserRepository _userRepository;
        readonly IOperationClaimServices _operationClaimServices;
        public TestController(BaseDbContext dbContext, ISmsSettingsService smsSettingsService, ISmsDefaultTemplateRepository defaultTemplateRepository, IUserRepository userRepository, IOperationClaimServices operationClaimServices)
        {
            _dbContext = dbContext;
            _smsSettingsService = smsSettingsService;
            _defaultTemplateRepository = defaultTemplateRepository;
            _userRepository = userRepository;
            _operationClaimServices = operationClaimServices;


        }
        [HttpPost("CreateClaim")]
        public async Task<IActionResult> CreateClaim(Guid id, string roleName)
        {
            User? user = await _userRepository.GetAsync(x=>x.Id==id);

            var roles = await _dbContext.UserOperationClaims.ToListAsync();
          var str=  await _operationClaimServices.AddUserRoleIfNotExistsAsync(user, roleName);
            return Ok(str);
        }
        [HttpGet]
        public async Task<IActionResult> SmsGönder([FromQuery] SmsEventType smsEventType)
        {
          var users =  await _dbContext.Users.ToListAsync();
            var id = (Guid)getUserIdFromRequest();
        bool statu=  await  _smsSettingsService.SMSSettingsControlAsync(id, smsEventType);
            return Ok(statu);
        }
        [HttpGet("template")]
        public async Task<IActionResult> Template([FromQuery]SmsEventType smsEventType)
        {
            var temlate = await _defaultTemplateRepository.GetTemplateByNameAsync(smsEventType);
           
            return Ok(temlate);
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedData()
        {
            if (_dbContext.SmsDefaultTemplates.Any() || _dbContext.SmsDefaultTemplates.Any())
            {
                return BadRequest("Veriler zaten eklenmiş.");
            }

            // Default SMS şablonları ekle
            _dbContext.SmsDefaultTemplates.AddRange(new List<SmsDefaultTemplate>
        {
            new SmsDefaultTemplate { SmsEventType = SmsEventType.ProductReceived, Content = "Ürünleriniz teslim alınmıştır." },
            new SmsDefaultTemplate { SmsEventType = SmsEventType.ProductIsReady, Content = "Ürününüz teslim edilmek için hazırdır." },
        });


            await _dbContext.SaveChangesAsync();

            return Ok("Veriler başarıyla eklendi.");
        }
        [HttpPost("smsSetting")]
        public async Task<IActionResult> AddSmsSettings()
        {

            if (!_dbContext.SmsSettingies.Any())
            {
                var smsSetting = new SmsSettings
                {
                    UserId =(Guid)getUserIdFromRequest(),
                    ProductReceived = true,
                    ProductIsReady = true
                    
                };

               await _dbContext.SmsSettingies.AddAsync(smsSetting);
                await _dbContext.SaveChangesAsync();
            }
            return Ok("Veriler başarıyla eklendi.");

        }
        [HttpPost("AppSetting")]
        public async Task<IActionResult> AppSetting()
        {
          var appSettings = new List<AppSetting>()
            {
              new AppSetting { Key ="ServerUTC", Value="+3"},
              new AppSetting { Key ="RegisterNewUser", Value="true"},
              new AppSetting { Key ="RegisterNewUserEmailVerification", Value="true"},
              new AppSetting { Key ="ProductStore", Value="false"},
              new AppSetting { Key ="Subscribers", Value="true"},
              new AppSetting { Key ="SendSms", Value="true"},
              new AppSetting { Key ="SendMail", Value="true"},
              new AppSetting { Key ="NewCompanySmsCount", Value="20"},
              new AppSetting { Key ="NewCompanyDayCount", Value="7"},
              new AppSetting { Key ="NewCompanyCompanyCount", Value="1"},
              new AppSetting { Key ="SmsReceivingOrder", Value="Sayın {customerName}, ürünleriniz {day} gün içerisinde alınacaktır."},
               

            };
            await _dbContext.AppSettings.AddRangeAsync(appSettings);
          await  _dbContext.SaveChangesAsync(true);
            return Ok(appSettings);
        }
        
    }

}
