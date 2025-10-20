using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
