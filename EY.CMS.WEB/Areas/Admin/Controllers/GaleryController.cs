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
    public class GaleryController : Controller
    {
        private readonly IService<Gallery> _service;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;

        public GaleryController(IService<Gallery> service, IMapper mapper, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb)
        {
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
        }

        public async Task<IActionResult> Index()
        {
            var galleries = await _service.GetAllAsync();
            var galleriesMapping = _mapper.Map<List<GalleryDto>>(galleries);
            return View(galleriesMapping);
        }
        public IActionResult Added()
        {
            return View();
        }

        public async Task<JsonResult> GalerySave(GalleryDto model, IList<IFormFile> file)
        {

            List<GalleryDto> galleries = new List<GalleryDto>();

            foreach (IFormFile source in file)
            {
                string fileName = Guid.NewGuid().ToString() + source.FileName.Substring(source.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/galery_img\\" + fileName))

                    source.CopyTo(output);
                model.Image = fileName;
                galleries.Add(model);
            }

            var galeryMaping = _mapper.Map<List<Gallery>>(galleries);

            await _service.AddRangeAsync(galeryMaping);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var galery = await _service.GetByIdAsync(id);
            TempData["imagePath"] = galery.Image;
            var galeryMapping = _mapper.Map<GalleryDto>(galery);
            return View(galeryMapping);

        }
        public async Task<JsonResult> GaleryEdit(GalleryDto model, IFormFile file)
        {

            model.Id = Convert.ToInt32(TempData["id"]);

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/galery_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            else
            {
                model.Image = TempData["imagePath"].ToString();
            }

            var galeryModel = _mapper.Map<Gallery>(model);
            await _service.UpdateAsync(galeryModel);

            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var galery = await _service.GetByIdAsync(id);
            TempData["imagePath"] = galery.Image;
            var galeryDto = _mapper.Map<GalleryDto>(galery);
            return View(galeryDto);
        }

        [HttpPost]
        public async Task<JsonResult> GaleryDelete(GalleryDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            model.Image = Path.Combine(_iweb.WebRootPath, "img/galery_img/", TempData["imagePath"].ToString());
            FileInfo fi = new FileInfo(TempData["imagePath"].ToString());
            if (fi != null)
            {
                System.IO.File.Delete(model.Image);
                fi.Delete();
            }

            var galery = _mapper.Map<Gallery>(model);
            await _service.RemoveAsync(galery);
            return Json(true);
        }

    }
}
