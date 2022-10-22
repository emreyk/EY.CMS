using Microsoft.AspNetCore.Mvc;

namespace EY.CMS.WEB.Controllers
{
    public class AccessDenied : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
