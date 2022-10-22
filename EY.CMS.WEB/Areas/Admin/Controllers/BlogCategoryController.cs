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
    public class BlogCategoryController : Controller
    {
        private readonly IService<Blog_Category> _service;
        private readonly IMapper _mapper;
        public BlogCategoryController(IService<Blog_Category> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            var categoryMapping = _mapper.Map<List<BlogCategoryDto>>(categories);
            return View(categoryMapping);
        }
        public IActionResult Added()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CategorySave(BlogCategoryDto model)
        {
            var category = _mapper.Map<Blog_Category>(model);
            await _service.AddAsync(category);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var category = await _service.GetByIdAsync(id);
            var categoryMapping = _mapper.Map<BlogCategoryDto>(category);
            return View(categoryMapping);
        }

        public async Task<JsonResult> CategoryEdit(BlogCategoryDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            var categoryModel = _mapper.Map<Blog_Category>(model);
            await _service.UpdateAsync(categoryModel);
            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var category = await _service.GetByIdAsync(id);
            var categoryMapping = _mapper.Map<BlogCategoryDto>(category);
            return View(categoryMapping);
        }

        public async Task<JsonResult> CategoryDelete(BlogCategoryDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            var category = _mapper.Map<Blog_Category>(model);
            await _service.RemoveAsync(category);
            return Json(true);
        }
    }
}
