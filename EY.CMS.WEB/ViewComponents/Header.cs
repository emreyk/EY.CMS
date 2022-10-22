using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Models;
using EY.CMS.CORE.Services;
using EY.CMS.WEB.Languages;
using EY.CMS.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EY.CMS.WEB.ViewComponents
{
    public class Header:ViewComponent
    {
        private readonly IService<Setting> _settingService;
        private readonly IService<Page> _pageService;
        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Lang> _stringLocalizer;
        public Header(IService<Setting> settingService, IService<Page> pageService, IService<Product> productService,
            IStringLocalizer<Lang> stringLocalizer,  IMapper mapper)
        {
            _settingService = settingService;
            _pageService = pageService;
            _productService = productService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public  IViewComponentResult Invoke()
        {
            HeaderViewModel model = new HeaderViewModel();

            var pages = _pageService.GetAllAsync().Result;
            var pagesModel = _mapper.Map<List<PageDto>>(pages);

            var setting = _settingService.GetAllAsync().Result.FirstOrDefault();
            var settingModel = _mapper.Map<SettingDto>(setting);

            var products = _productService.GetAllAsync().Result;
            var productsModel = _mapper.Map<List<ProductDto>>(products);

            model.Pages = pagesModel;
            model.Setting = settingModel;
            model.Products = productsModel;

            ViewBag.HomePage = _stringLocalizer["HomePage"];
            ViewBag.ContactUs = _stringLocalizer["ContactUs"];
            ViewBag.Products = _stringLocalizer["Products"];
            ViewBag.Contact = _stringLocalizer["Contact"];
            ViewBag.AboutUs = _stringLocalizer["AboutUs"];

            string culture = HttpContext.Request.Query["culture"];
            ViewBag.Culture = culture;
            return View(model);
        }
    }
}
