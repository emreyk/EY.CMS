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
    public class BlogController : Controller
    {

        private readonly IBlogService _blogService;
        private readonly IService<Blog_Category> _service;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;

        public BlogController(IBlogService blogService, IService<Blog_Category> service, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb, IMapper mapper)
        {
            _blogService = blogService;
            _service = service;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
        }
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetBlogsWithCategory();
            return View(blogs);
        }
        public async Task<IActionResult> Added()
        {
            var blogCategory = await _service.GetAllAsync();
            var blogCategoryMapping = _mapper.Map<List<BlogCategoryDto>>(blogCategory); 
            return View(blogCategoryMapping);
        }

        public async Task<JsonResult> BlogSave(BlogWithCategoryDto model, IFormFile file)
        {
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/blog_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }

            var blogMaping = _mapper.Map<Blog>(model);
            await _blogService.AddAsync(blogMaping);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var blog = await _blogService.GetBlogById(id);
            TempData["imagePath"] = blog.Image;

            var blogMapping = _mapper.Map<BlogWithCategoryDto>(blog);
            var blogCategories = _mapper.Map<List<BlogCategoryDto>>(await _service.GetAllAsync());

            BlogViewModel viewModel = new BlogViewModel();
            viewModel.BlogWithCategory = blogMapping;
            viewModel.BlogCategories = blogCategories;

            return View(viewModel);
        }
        public async Task<JsonResult> BlogEdit(BlogWithCategoryDto model, IFormFile file)
        {
            model.Id = Convert.ToInt32(TempData["id"]);

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/blog_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            else
            {
                model.Image = TempData["imagePath"].ToString();
            }

            var blogModel = _mapper.Map<Blog>(model);
            await _blogService.UpdateAsync(blogModel);

            return Json(true);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var blog = await _blogService.GetBlogById(id);
            TempData["imagePath"] = blog.Image;
            return View(blog);
        }

        [HttpPost]
        public async Task<JsonResult> BlogDelete(BlogWithCategoryDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            model.Image = Path.Combine(_iweb.WebRootPath, "img/blog_img/", TempData["imagePath"].ToString());
            FileInfo fi = new FileInfo(TempData["imagePath"].ToString());
            if (fi != null)
            {
                System.IO.File.Delete(model.Image);
                fi.Delete();
            }

            var blog = _mapper.Map<Blog>(model);
            await _blogService.RemoveAsync(blog);
            return Json(true);
        }

    }
}
