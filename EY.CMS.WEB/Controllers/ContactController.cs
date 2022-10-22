using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Services;
using EY.CMS.WEB.Languages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EY.CMS.WEB.Controllers
{
    public class ContactController : Controller
    {
        private readonly IService<Setting> _settingService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Lang> _stringLocalizer;
        public ContactController(IService<Setting> settingService, IStringLocalizer<Lang> stringLocalizer, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }


        [Route("/iletisim")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Contact = _stringLocalizer["Contact"];
            ViewBag.ContactUs = _stringLocalizer["ContactUs"];
            ViewBag.Name = _stringLocalizer["Name"];
            ViewBag.Phone = _stringLocalizer["Phone"];
            ViewBag.Description = _stringLocalizer["Description"];
            ViewBag.Location = _stringLocalizer["Location"];
            ViewBag.SendMessage = _stringLocalizer["SendMessage"];

            var setting = await _settingService.GetAllAsync();
            var settingModel = _mapper.Map<SettingDto>(setting.FirstOrDefault());
            return View(settingModel);
        }
    }
}
