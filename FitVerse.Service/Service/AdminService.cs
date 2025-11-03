using AutoMapper;
using FitVerse.Core.IService;
using FitVerse.Core.Models;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Admin;
using FitVerse.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Service.Service
{
    public class AdminService : FitVerse.Core.IService.IAdminService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;


        public AdminService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) 
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }


        public int getCoachesCount()
        {
            return unitOfWork.Coaches.GetAll().Count();
        }

        public int getSoldedPackagesCount()
        {
            return unitOfWork.Payments.GetAll().Count();
        }

        public decimal getTotalRevenue()
        {
            return unitOfWork.Payments.GetAll().Sum(p => (decimal)p.Amount);
        }

        public int getTotalUsersCount()
        {
            return userManager.Users.Count();
        }

        public List<TopCoachViewModel> getTopRatedCoaches()
        {
            // Get all coaches with their User data
            var coachesWithUsers = unitOfWork.Coaches
                .GetAll(includeProperties: "User,ClientSubscriptions,CoachFeedbacks")
                .ToList();

            // Get feedback statistics
            var feedbackStats = unitOfWork.CoachFeedbacks.GetAll()
                .GroupBy(fb => fb.CoachId)
                .Select(g => new
                {
                    CoachId = g.Key,
                    AverageRate = g.Average(fb => fb.Rate),
                    FeedbackCount = g.Count()
                })
                .ToList();

            // Join and create view models
            var topCoaches = feedbackStats
                .Join(coachesWithUsers,
                      feedback => feedback.CoachId,
                      coach => coach.Id,
                      (feedback, coach) => new TopCoachViewModel
                      {
                          CoachId = coach.Id,
                          Name = coach.User?.FullName ?? "Unknown Coach",
                          ImagePath = coach.User?.ImagePath ?? "",
                          ClientsCount = coach.ClientSubscriptions?.Count(cs => cs.Status == "Active") ?? 0,
                          Rating = feedback.AverageRate,
                          FeedbackCount = feedback.FeedbackCount
                      })
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.ClientsCount)
                .Take(3) // Top 3 coaches only
                .ToList();

            return topCoaches;
        }


    }



}
