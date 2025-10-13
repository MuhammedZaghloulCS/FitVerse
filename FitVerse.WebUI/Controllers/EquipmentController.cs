using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
