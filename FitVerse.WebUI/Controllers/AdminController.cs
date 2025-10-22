using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
