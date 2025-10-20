using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
