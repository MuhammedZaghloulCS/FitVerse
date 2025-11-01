using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.ViewModels.Plan;
using Microsoft.AspNetCore.Mvc;

namespace FitVerse.Web.Controllers
{
    public class ExercisePlanController : Controller
    {
        private readonly IExercisePlanService _exercisePlanService;
        private readonly IExerciseService _exerciseService;
        private readonly IMapper _mapper;

        public ExercisePlanController(
            IExercisePlanService exercisePlanService,
            IExerciseService exerciseService,
            IMapper mapper)
        {
            _exercisePlanService = exercisePlanService;
            _exerciseService = exerciseService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var plans = _exercisePlanService.GetAllPlans();
            return View(plans);
        }
        [HttpGet]
        public IActionResult GetAllExercises()
        {
            var exercises = _exerciseService.GetAllExercises()
                .Select(e => new
                {
                    e.Id,
                    e.Name
                });

            return Json(exercises);
        }

        public IActionResult Details(int id)
        {
            var plan = _exercisePlanService.GetPlanById(id);
            if (plan == null)
                return NotFound();

            return View(plan);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Exercises = _exerciseService.GetAllExercises().ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreatePlan([FromBody] ExercisePlanVM vm)
        {
            try
            {
                var success = _exercisePlanService.CreatePlan(vm);
                return Ok(new { message = "Plan created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var plan = _exercisePlanService.GetPlanById(id);
            if (plan == null)
                return NotFound();

            var vm = _mapper.Map<ExercisePlanVM>(plan);
            ViewBag.Exercises = _exerciseService.GetAllExercises().ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(int id, ExercisePlanVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Exercises = _exerciseService.GetAllExercises().ToList();
                return View(vm);
            }

            var success = _exercisePlanService.UpdatePlan(id, vm);
            if (success)
                return RedirectToAction(nameof(Index));

            TempData["Error"] = "something went wrong ";
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var plan = _exercisePlanService.GetPlanById(id);
            if (plan == null)
                return NotFound();

            return View(plan);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
      
        public IActionResult DeleteConfirmed(int id)
        {
            var success = _exercisePlanService.DeletePlan(id);
            if (success)
                return RedirectToAction(nameof(Index));

            TempData["Error"] = "something went wrong ";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetAllPlans()
        {
            var plans = _exercisePlanService.GetAllPlans()
                .Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    durationWeeks = p.DurationWeeks,
                    notes = p.Notes,
                    clientCount = p.ClientId?.Count() ?? 0
                })
                .ToList();

            return Json(plans);
        }


    }
}
