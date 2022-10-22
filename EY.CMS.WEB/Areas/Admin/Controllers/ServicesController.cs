using AutoMapper;
using EY.CMS.CORE;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly IService<Service> _service;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;
        public ServicesController(IService<Service> service, IMapper mapper, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb)
        {
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
        }


        public async Task<IActionResult> Index()
        {
            var services = await _service.GetAllAsync();
            var servicesMapping = _mapper.Map<List<ServiceDto>>(services);
            return View(servicesMapping);
        }
        public IActionResult Added()
        {
            return View();
        }

        public async Task<JsonResult> ServiceSave(ServiceDto model, IFormFile file)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/service_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }

            else
            {
                model.Image = null;
            }

            var serviceMaping = _mapper.Map<Service>(model);

            await _service.AddAsync(serviceMaping);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var service = await _service.GetByIdAsync(id);
            TempData["imagePath"] = service.Image;

            var serviceMapping = _mapper.Map<ServiceDto>(service);
            return View(serviceMapping);
        }

        [HttpPost]
        public async Task<JsonResult> ServiceEdit(ServiceDto model, IFormFile file)
        {
            model.Id = Convert.ToInt32(TempData["id"]);

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/service_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            else
            {
                model.Image = TempData["imagePath"].ToString();
            }

            var serviceModel = _mapper.Map<Service>(model);
            await _service.UpdateAsync(serviceModel);

            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var service = await _service.GetByIdAsync(id);
            TempData["imagePath"] = service.Image;
            var serviceDto = _mapper.Map<ServiceDto>(service);
            return View(serviceDto);
        }

        [HttpPost]
        public async Task<JsonResult> ServiceDelete(ServiceDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            model.Image = Path.Combine(_iweb.WebRootPath, "img/service_img/", TempData["imagePath"].ToString());
            FileInfo fi = new FileInfo(TempData["imagePath"].ToString());
            if (fi != null)
            {
                System.IO.File.Delete(model.Image);
                fi.Delete();
            }

            var service = _mapper.Map<Service>(model);
            await _service.RemoveAsync(service);
            return Json(true);
        }
    }
}
