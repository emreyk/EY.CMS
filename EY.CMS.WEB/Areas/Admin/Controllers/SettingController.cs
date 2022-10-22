using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController : Controller
    {
        private readonly IService<Setting> _service;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;

        public SettingController(IService<Setting> service, IMapper mapper, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb)
        {
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
        }

        public async Task<IActionResult> Index()
        {
            var setting = _mapper.Map<List<SettingDto>>(await _service.GetAllAsync()).FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        public async Task<JsonResult> SettingEdit(SettingDto model, IFormFile file)
        {
            var setting = _mapper.Map<List<SettingDto>>(await _service.GetAllAsync()).FirstOrDefault();
            if (setting == null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img\\" + fileName))
                    file.CopyTo(output);
                model.Logo = fileName;
                var settingMapingModel = _mapper.Map<SettingDto, Setting>(model);
                await _service.AddAsync(settingMapingModel);
                return Json(true);
            }
            else
            {
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img\\" + fileName))
                        file.CopyTo(output);
                    model.Logo = fileName;
                }
                else
                {
                    model.Logo = setting.Logo;
                }
                model.Id = setting.Id;
                var settingMapingModel = _mapper.Map<SettingDto, Setting>(model);
                await _service.UpdateAsync(settingMapingModel);
                return Json(true);
            }
        }
    }
}
