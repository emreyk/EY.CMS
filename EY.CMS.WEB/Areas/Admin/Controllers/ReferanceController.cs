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
    public class ReferanceController: Controller
    {
        private readonly IService<Referance> _service;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;

        public ReferanceController(IService<Referance> service, IMapper mapper, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb)
        {
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
        }

        public async Task<IActionResult> Index()
        {
            var referances = await _service.GetAllAsync();
            var referancesMapping = _mapper.Map<List<ReferanceDto>>(referances);
            return View(referancesMapping);
        }
        public IActionResult Added()
        {
            return View();
        }


        public async Task<JsonResult> ReferanceSave(ReferanceDto model, IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
            using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/referance_img\\" + fileName))
                file.CopyTo(output);
            model.Image = fileName;
            var referanceMaping = _mapper.Map<Referance>(model);

            await _service.AddAsync(referanceMaping);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var referance = await _service.GetByIdAsync(id);
            TempData["imagePath"] = referance.Image;
            var referanceMapping = _mapper.Map<ReferanceDto>(referance);
            return View(referanceMapping);
        }

        [HttpPost]
        public async Task<JsonResult> ReferanceEdit(ReferanceDto model, IFormFile file)
        {

            model.Id = Convert.ToInt32(TempData["id"]);

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/referance_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            else
            {
                model.Image = TempData["imagePath"].ToString();
            }

            var referanceModel = _mapper.Map<Referance>(model);
            await _service.UpdateAsync(referanceModel);

            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var referance = await _service.GetByIdAsync(id);
            TempData["imagePath"] = referance.Image;
            var referanceDto = _mapper.Map<ReferanceDto>(referance);
            return View(referanceDto);
        }

        [HttpPost]
        public async Task<JsonResult> ReferanceDelete(ReferanceDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            model.Image = Path.Combine(_iweb.WebRootPath, "img/referance_img/", TempData["imagePath"].ToString());
            FileInfo fi = new FileInfo(TempData["imagePath"].ToString());
            if (fi != null)
            {
                System.IO.File.Delete(model.Image);
                fi.Delete();
            }

            var referance = _mapper.Map<Referance>(model);
            await _service.RemoveAsync(referance);
            return Json(true);
        }
    }
}
