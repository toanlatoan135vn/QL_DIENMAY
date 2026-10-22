using Microsoft.AspNetCore.Mvc;

namespace ql_cuahang.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}