using EY.CMS.CORE;
using EY.CMS.CORE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        

        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
