using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ExersiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
