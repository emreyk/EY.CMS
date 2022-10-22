using AutoMapper;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Controllers
{
    public class AllProductController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;

        public AllProductController(IService<Product> productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Route("/tumurunler")]
        public IActionResult Index(string seourl)
        {
            var product = _productService.Where(x=>x.ProductParentId == 0).ToList();
            var productMapping = _mapper.Map<List<ProductDto>>(product);
            return View(productMapping);
        }
    }
}
