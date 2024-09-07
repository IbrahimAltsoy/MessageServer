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

        public TestController(BaseDbContext dbContext, ISmsSettingsService smsSettingsService, ISmsDefaultTemplateRepository defaultTemplateRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _smsSettingsService = smsSettingsService;
            _defaultTemplateRepository = defaultTemplateRepository;
            _userRepository = userRepository;

        }
        [HttpGet]
        public async Task<IActionResult> SmsGönder([FromQuery] SmsEventType smsEventType)
        {
          var users =  await _dbContext.Users.ToListAsync();
          await  _smsSettingsService.SMSSettingsControlAsync(Guid.Parse("0fa73515-bc2d-46da-93ca-efb61eaee7b0"), smsEventType);
            return Ok(users);
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
        
    }

}
