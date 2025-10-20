using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Areas.Coach.Controllers
{
    [Area("Coach")]
    public class CoachController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
