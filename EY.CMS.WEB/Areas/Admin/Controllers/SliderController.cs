using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly IService<Slider> _service;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;

        public SliderController(IService<Slider> service, IMapper mapper, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb)
        {
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _service.GetAllAsync();
            var sliderMapping = _mapper.Map<List<SliderDto>>(sliders);
            return View(sliderMapping);
        }
        public IActionResult Added()
        {
            return View();
        }


        public async Task<JsonResult> SliderSave(SliderDto model, IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
            using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/slider_img\\" + fileName))
                file.CopyTo(output);
            model.Image = fileName;
            var sliderMaping = _mapper.Map<Slider>(model);

            await _service.AddAsync(sliderMaping);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var slider = await _service.GetByIdAsync(id);
            TempData["imagePath"] = slider.Image;
            var sliderMapping = _mapper.Map<SliderDto>(slider);
            return View(sliderMapping);
        }
       
        [HttpPost]
        public  async Task<JsonResult> SliderEdit(SliderDto model, IFormFile file)
        {
           
            model.Id = Convert.ToInt32(TempData["id"]);

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/slider_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            else
            {
                model.Image = TempData["imagePath"].ToString();
            }

            var sliderModel = _mapper.Map<Slider>(model);
            await _service.UpdateAsync(sliderModel);

            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var slider = await _service.GetByIdAsync(id);
            TempData["imagePath"] = slider.Image;
            var slidersDto = _mapper.Map<SliderDto>(slider);
            return View(slidersDto);
        }

        [HttpPost]
        public async Task<JsonResult> SliderDelete(SliderDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            model.Image = Path.Combine(_iweb.WebRootPath, "img/slider_img/", TempData["imagePath"].ToString());
            FileInfo fi = new FileInfo(TempData["imagePath"].ToString());
            if (fi != null)
            {
                System.IO.File.Delete(model.Image);
                fi.Delete();
            }

            var slider = _mapper.Map<Slider>(model);
            await _service.RemoveAsync(slider);
            return Json(true);
        }
    }
}
