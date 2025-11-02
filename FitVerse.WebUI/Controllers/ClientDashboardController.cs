using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.ClientDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientDashboardController : Controller
    {
        private readonly IUnitOFWorkService _unitOfWorkService;

        public ClientDashboardController(IUnitOFWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public IActionResult Dashboard()
        {
            var model = _unitOfWorkService.ClientDashboardService.GetClientDashboard();
            if (model == null)
                return NotFound("Client or Coach not found");
            if (model.CoachName == null)
                return RedirectToAction("ClientCoaches", "Coach");

            return View(model);
        }
    }
}
