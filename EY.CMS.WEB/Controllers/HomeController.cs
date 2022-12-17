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
       
    

        private readonly IService<ErrorLog> _service;

        //EXAMPLE

        //private readonly IService<Slider> _serviceSlider;
        //private readonly IMapper _mapper;
        //private readonly IStringLocalizer<Lang> _stringLocalizer;

        //public HomeController(IMapper mapper, IService<ErrorLog> service,
        //    IService<Slider> serviceSlider, IStringLocalizer<Lang> stringLocalizer)
        //{
          

        //    _service = service;
        //    _serviceSlider = serviceSlider;
        //    _mapper = mapper;
        //    _stringLocalizer = stringLocalizer;
        //}

        [Route("")]
        [Route("Anasayfa")]
        public async Task<IActionResult> Index()
        {
            HomeModel model = new HomeModel();

            //EXAMPLE

            //var slider = await _serviceSlider.GetAllAsync();
            //var sliderMapping = _mapper.Map<List<SliderDto>>(slider);

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