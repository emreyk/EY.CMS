using AutoMapper;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using EY.CMS.REPOSITORY;
using EY.CMS.SERVICE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {

        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IWebHostEnvironment _iweb;
        protected readonly EyCmsContext _context;
        public ProductController(IService<Product> productService, IWebHostEnvironment hostEnvironment, IWebHostEnvironment iweb,
            IMapper mapper, EyCmsContext context)
        {
            _productService = productService;
            _mapper = mapper;
            _hostingEnvironment = hostEnvironment;
            _iweb = iweb;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prodcuts = await _productService.GetAllAsync();
            var prodcutsMapping = _mapper.Map<List<ProductDto>>(prodcuts);
            return View(prodcutsMapping);
        }
        public async Task<IActionResult> Added()
        {
            var prodcuts = await _productService.GetAllAsync();
            var prodcutsMapping = _mapper.Map<List<ProductDto>>(prodcuts);
            return View(prodcutsMapping);
        }
        public async Task<JsonResult> ProductSave(ProductDto model, IFormFile file, IFormFile fileBanner)
        {
            var product2 = await _productService.GetAllAsync();
            var product = product2.Where(x => x.Id == Convert.ToInt32(TempData["id"])).FirstOrDefault();

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/product_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            if (fileBanner != null)
            {
                string fileName = Guid.NewGuid().ToString() + fileBanner.FileName.Substring(fileBanner.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/product_img\\" + fileName))
                    fileBanner.CopyTo(output);
                model.BannerImage = fileName;
            }

            var productMaping = _mapper.Map<Product>(model);
            await _productService.AddAsync(productMaping);
            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TempData["id"] = id;
            var products = await _productService.GetAllAsync();
            var product = await _productService.GetByIdAsync(id);

            Models.ProductViewModel viewModel = new Models.ProductViewModel();

            var prodcutsMapping = _mapper.Map<List<ProductDto>>(products);
            var prodcutMapping = _mapper.Map<ProductDto>(product);

            viewModel.Products = prodcutsMapping;
            viewModel.Product = prodcutMapping;
            return View(viewModel);
        }
        public async Task<JsonResult> ProductEdit(ProductDto model, IFormFile file, IFormFile fileBanner)
        {
            var product2 = await _productService.GetAllAsync();
            var product = product2.Where(x => x.Id == Convert.ToInt32(TempData["id"])).FirstOrDefault();

            //var product = await _productService.GetByIdAsync(Convert.ToInt32(TempData["id"]));

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/product_img\\" + fileName))
                    file.CopyTo(output);
                model.Image = fileName;
            }
            if (file == null)
            {
                model.Image = product.Image;
            }
            if (fileBanner != null)
            {
                string fileName = Guid.NewGuid().ToString() + fileBanner.FileName.Substring(fileBanner.FileName.LastIndexOf("."));
                using (FileStream output = System.IO.File.Create(this._hostingEnvironment.WebRootPath + "\\img/product_img\\" + fileName))
                    fileBanner.CopyTo(output);
                model.BannerImage = fileName;
            }
            if (fileBanner == null)
            {
                model.BannerImage = product.BannerImage;
            }

            if (Convert.ToInt32(TempData["id"]) != model.Id)
            {
                model.ProductParentId = model.Id;
                model.Id = product.Id;
                
            }
           
            var productModel = _mapper.Map<Product>(model);
            await _productService.UpdateAsync(productModel);

            return Json(true);
        }
        public async Task<IActionResult> Delete(int id)
        {
            TempData["id"] = id;
            var product = await _productService.GetByIdAsync(id);
            var productMapping = _mapper.Map<ProductDto>(product);
            TempData["imagePath"] = product.Image;
            return View(productMapping);
        }


        public async Task<JsonResult> ProductDelete(ProductDto model)
        {
            model.Id = Convert.ToInt32(TempData["id"]);
            // model.Image = Path.Combine(_iweb.WebRootPath, "img/product_img/", TempData["imagePath"].ToString());
            if (TempData["imagePath"] != null)
            {
                FileInfo fi = new FileInfo(TempData["imagePath"].ToString());
                System.IO.File.Delete(model.Image);
                fi.Delete();
            }

            var blog = _mapper.Map<Product>(model);
            await _productService.RemoveAsync(blog);
            return Json(true);
        }
    }
}
