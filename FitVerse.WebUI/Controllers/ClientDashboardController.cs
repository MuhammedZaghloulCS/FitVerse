using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.ClientDashboard;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ClientDashboardController : Controller
    {
        private readonly IUnitOFWorkService _unitOfWorkService;

        public ClientDashboardController(IUnitOFWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public IActionResult Dashboard(string clientId = "1")
        {
            var model = _unitOfWorkService.ClientDashboardService.GetClientDashboardAsync(clientId);
            if (model == null)
                return NotFound("Client or Coach not found");

            return View(model);
        }
    }
}
