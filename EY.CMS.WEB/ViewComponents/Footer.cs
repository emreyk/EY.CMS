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
    public class Footer:ViewComponent
    {
        private readonly IService<Setting> _settingService;
        private readonly IService<Product> _productService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Lang> _stringLocalizer;

        public Footer(IService<Setting> settingService,IService<Product> productService, IStringLocalizer<Lang> stringLocalizer, IMapper mapper)
        {
            _settingService = settingService;
            _productService = productService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer; 
        }

        public IViewComponentResult Invoke()
        {
            HeaderViewModel viewModel = new HeaderViewModel();

            var setting = _settingService.GetAllAsync().Result.FirstOrDefault();
            var settingModel = _mapper.Map<SettingDto>(setting);

            var product = _productService.Where(x => x.ProductParentId == 0).ToList();
            var productMapping = _mapper.Map<List<ProductDto>>(product);

            viewModel.Setting = settingModel;
            viewModel.Products = productMapping;

            string culture = HttpContext.Request.Query["culture"];
            ViewBag.Culture = culture;

            ViewBag.AllProducts = _stringLocalizer["Products"];
            ViewBag.Contact = _stringLocalizer["Contact"];
            ViewBag.UsefulLinks = _stringLocalizer["UsefulLinks"];
            ViewBag.Company = _stringLocalizer["Company"];
            ViewBag.Products = _stringLocalizer["Products"];
            ViewBag.HomePage = _stringLocalizer["HomePage"];
            return View(viewModel);
        }
    }
}
