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
    public class PageController : Controller
    {
        private readonly IService<Page> _service;
        private readonly IMapper _mapper;
        public PageController(IService<Page> service, IMapper mapper, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var pages = await _service.GetAllAsync();
            var pageMapping = _mapper.Map<List<PageDto>>(pages);
            return View(pageMapping);
        }
        public IActionResult Added()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> PageSave(PageDto model)
        {
            var page = _mapper.Map<Page>(model);
            await _service.AddAsync(page);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var page = await _service.GetByIdAsync(id);
            var pageMapping = _mapper.Map<PageDto>(page);
            return View(pageMapping);
        }

        [HttpPost]
        public async Task<JsonResult> PageEdit(PageDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            var pageModel = _mapper.Map<Page>(model);
            await _service.UpdateAsync(pageModel);
            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var page = await _service.GetByIdAsync(id);
            var pagesMapping = _mapper.Map<PageDto>(page);
            return View(pagesMapping);
        }

        [HttpPost]
        public async Task<JsonResult> PageDelete(PageDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            var page = _mapper.Map<Page>(model);
            await _service.RemoveAsync(page);
            return Json(true);
        }
    }
}
