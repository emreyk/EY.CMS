using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using EY.CMS.WEB.Languages;
using EY.CMS.WEB.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace EY.CMS.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<ErrorLog> _service;
        private readonly IService<Slider> _serviceSlider;
        private readonly IService<Product> _serviceProduct;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Lang> _stringLocalizer;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IService<ErrorLog> service, 
            IService<Slider> serviceSlider, IStringLocalizer<Lang> stringLocalizer, IService<Product> serviceProduct)
        {
            _logger = logger;
            _service = service;
            _serviceSlider = serviceSlider;
            _serviceProduct = serviceProduct;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        [Route("")]
        [Route("Anasayfa")]
        public async Task<IActionResult> Index()
        {
            HomeModel model = new HomeModel();

            var slider = await _serviceSlider.GetAllAsync();
            var sliderMapping = _mapper.Map<List<SliderDto>>(slider);

            var products = _serviceProduct.Where(x => x.ProductParentId == 0 && x.SeoUrl != "oil-urunleri").OrderByDescending(x => x.Id).ToList();
            var allProducts = await _serviceProduct.GetAllAsync();

            var productsMapping = _mapper.Map<List<ProductDto>>(products);
            var allProductsMapping = _mapper.Map<List<ProductDto>>(allProducts);

            model.Sliders = sliderMapping;
            model.Product = productsMapping;
            model.AllProduct = allProductsMapping;

            string culture = HttpContext.Request.Query["culture"];
            ViewBag.Culture = culture;

            ViewBag.WhatAreWeDoing = _stringLocalizer["WhatAreWeDoing"];
            ViewBag.WhatAreWeDoingSub = _stringLocalizer["WhatAreWeDoingSub"];
            ViewBag.AllProducts = _stringLocalizer["AllProducts"];
            ViewBag.TheBest = _stringLocalizer["TheBest"];
            ViewBag.TheBestFirst = _stringLocalizer["TheBestFirst"];
            ViewBag.TheBestSecond = _stringLocalizer["TheBestSecond"];
            ViewBag.TheBestThird = _stringLocalizer["TheBestThird"];
            ViewBag.TheBestDesc = _stringLocalizer["TheBestDesc"];
            ViewBag.HaveProblem = _stringLocalizer["HaveProblem"];
            ViewBag.HesitateContact = _stringLocalizer["HesitateContact"];
            ViewBag.ContactUs = _stringLocalizer["ContactUs"];
            ViewBag.Tank = _stringLocalizer["Tank"];
            ViewBag.TankDesc = _stringLocalizer["TankDesc"];
            ViewBag.TankFirst = _stringLocalizer["TankFirst"];
            ViewBag.TankSecond = _stringLocalizer["TankSecond"];
            ViewBag.TankThird = _stringLocalizer["TankThird"];
            ViewBag.HomeEnd = _stringLocalizer["HomeEnd"];
          
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Error()
        {
            var exceptionHandlerPathFeature =
             HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        
            ErrorLog model = new ErrorLog();
            model.Path = exceptionHandlerPathFeature.Path;
            model.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            model.Date = DateTime.Now.Date;
            await _service.AddAsync(model);
            return null;
        }
    }
}