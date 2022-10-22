using AutoMapper;
using Ey.Cms.CORE.Models;
using EY.CMS.CORE.DTOs;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Controllers
{
    public class PageController : Controller
    {
        private readonly IService<Page> _pageService;
        private readonly IMapper _mapper;

        public PageController(IService<Page> pageService, IMapper mapper)
        {
            _pageService = pageService;
            _mapper = mapper;
        }

        [Route("/sayfa/{seourl}")]
        public IActionResult Index(string seourl)
        {
            string culture = HttpContext.Request.Query["culture"];
            ViewBag.Culture = culture;

            var page =  _pageService.Where(x=>x.SeoUrl == seourl).FirstOrDefault();
            var pageMapping = _mapper.Map<PageDto>(page);
            return View(pageMapping);
        }
    }
}
