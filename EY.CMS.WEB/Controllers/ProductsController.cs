using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Controllers
{
  
    public class ProductsController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;

        public ProductsController(IService<Product> productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Route("/urunler/{seourl}")]
        public  IActionResult Index(string seourl)
        {
            string culture = HttpContext.Request.Query["culture"];
            ViewBag.Culture = culture;
            ViewBag.SeoUrl = seourl;

            var productId = _productService.Where(x => x.SeoUrl == seourl).FirstOrDefault().Id;
            var categoryProducts = _productService.Where(x => x.ProductParentId == productId).ToList();
            var productMapping = _mapper.Map<List<ProductDto>>(categoryProducts);
            return View(productMapping);
        }
    }
}
