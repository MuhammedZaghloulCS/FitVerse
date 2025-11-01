using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.ViewModels.DietPlan;
using FitVerse.Data.Models;
using FitVerse.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class DietPlanController : Controller
    {
        private readonly IUnitOFWorkService _unitOfWorkService;
        public DietPlanController(IUnitOFWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        public IActionResult Index()
        {
            var model = new DietPlanDashboardVM
            {
                DietPlanCount = _unitOfWorkService.DietPlanService.GetCount(),
                ClientFollowingCount = _unitOfWorkService.DietPlanService.GetClientFollowing()

            };
            return View(model);
        }
        [HttpGet]
        public IActionResult GetAll(string? search)
        {
            var plans = _unitOfWorkService.DietPlanService.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                plans = plans.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return Json(new { data = plans });
        }

        public IActionResult GetById(int id)
        {
            var plan = _unitOfWorkService.DietPlanService.GetById(id);
            if (plan == null)
                return Json(null);

            return Json(plan); // دلوقتي res في JS هتبقى الخطة مباشرة
        }
        [HttpPost]
        public IActionResult Add([FromBody] DietPlanVM plan)
        {
            try
            {
                _unitOfWorkService.DietPlanService.Add(plan);
                return Json(new { success = true, message = "Diet Plan added successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, stack = ex.StackTrace });
            }
        }

        public IActionResult Update([FromBody] DietPlanVM plan)
        {
            try
            {
                if (plan == null || plan.Id == 0)
                    return Json(new { success = false, message = "Invalid diet plan data." });

                _unitOfWorkService.DietPlanService.Update(plan);
                return Json(new { success = true, message = "Diet Plan updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Delete(int id)
        {
            _unitOfWorkService.DietPlanService.Delete(id);
            return Json(new { success = true, message = "Diet Plan deleted successfully." });
        }
        



    }
}
