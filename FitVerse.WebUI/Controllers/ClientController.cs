using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOFWorkService unitOFWorkService;

       

        public ClientController(IUnitOFWorkService unitOFWorkService)
        {
            this.unitOFWorkService = unitOFWorkService;

        }
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult GetAll()
        {
            var clients = unitOFWorkService.ClientServices.GetAllClients();
            return Json(new { data = clients });
        }

      
        public IActionResult Add(AddClientVM model)
        {
            var result = unitOFWorkService.ClientServices.AddClient(model);
            return Json(result);
        }
    }
}
