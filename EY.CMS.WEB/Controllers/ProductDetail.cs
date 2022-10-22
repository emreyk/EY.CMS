using AutoMapper;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Controllers
{
    public class ProductDetail : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;

        public ProductDetail(IService<Product> productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Route("/urun-detay/{seourl}")]
        public IActionResult Index(string seourl)
        {
            string culture = HttpContext.Request.Query["culture"];
            ViewBag.Culture = culture;

            var product = _productService.Where(x => x.SeoUrl == seourl).FirstOrDefault();
            var productMapping = _mapper.Map<ProductDto>(product);
            return View(productMapping);
        }
    }
}
