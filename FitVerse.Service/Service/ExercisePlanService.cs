using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.IService;
using FitVerse.Core.IUnitOfWorkServices;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.ExerciseVM;
using FitVerse.Core.ViewModels.Plan;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class ExercisePlanService : IExercisePlanService
    {

        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public ExercisePlanService(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IEnumerable<ExercisePlan> GetAllPlans()
        {
            return _db.ExercisePlans.GetAll().ToList();
        }

      
        public ExercisePlan GetPlanById(int id)
        {
            return _db.ExercisePlans.GetById(id);
        }



        public bool UpdatePlan(int id, ExercisePlanVM vm)
        {
            try
            {
                var plan = _db.ExercisePlans.GetById(id);
                if (plan == null)
                    return false;

                // Update main plan fields
                plan.Name = vm.Name;
                plan.DurationWeeks = vm.DurationWeeks;
                plan.Notes = vm.Notes;

                // Optional: update exercise details if needed
                if (vm.SelectedExerciseIds != null)
                {
                    plan.ExercisePlanDetails.Clear();
                    foreach (var exId in vm.SelectedExerciseIds)
                    {
                        plan.ExercisePlanDetails.Add(new ExercisePlanDetail
                        {
                            ExerciseId = exId,
                            NumOfSets = vm.DefaultSets > 0 ? vm.DefaultSets : 3,
                            NumOfRepeats = vm.DefaultRepeats > 0 ? vm.DefaultRepeats : 10,
                            Date = DateTime.Now,
                            IsCompleted = false
                        });
                    }
                }

                _db.ExercisePlans.Update(plan);
                return _db.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePlan(int id)
        {
            try
            {
                var plan = _db.ExercisePlans.GetById(id);
                if (plan == null)
                    return false;

                _db.ExercisePlans.Delete(plan);
                return _db.Complete() > 0;
            }
            catch
            {
                return false;
            }
        }



        public bool CreatePlan(ExercisePlanVM vm)
        {

            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            try
            {
                var plan = new ExercisePlan
                {
                    Name = vm.Name,
                    Notes = vm.Notes,
                    DurationWeeks = vm.DurationWeeks,
                    CoachId = vm.CoachId?.ToString() ?? "default_coach_id", 
                    ClientId = vm.ClientId?.ToString() ?? "default_client_id",
                    Date = DateTime.Now
                };

                if (vm.SelectedExerciseIds != null && vm.SelectedExerciseIds.Any())
                {
                    plan.ExercisePlanDetails = vm.SelectedExerciseIds.Select(exId => new ExercisePlanDetail
                    {
                        ExerciseId = exId,
                        NumOfSets = vm.DefaultSets,
                        NumOfRepeats = vm.DefaultRepeats,
                        Date = DateTime.Now
                    }).ToList();
                }

                _db.ExercisePlans.Add(plan);
                _db.Complete();

                return true;
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new Exception("CreatePlan failed: " + inner);
            }
        }
    }

}