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
            var topCoaches = unitOfWork.CoachFeedbacks.GetAll()
                .GroupBy(fb => fb.CoachId)
                .Select(g => new
                {
                    CoachId = g.Key,
                    AverageRate = g.Average(fb => fb.Rate),
                    ClientsCount = g.Count()
                })
                .OrderByDescending(x => x.AverageRate)
                .Take(5)
                .Join(unitOfWork.Coaches.GetAll(),
                      avg => avg.CoachId,
                      coach => coach.Id,
                      (avg, coach) => new TopCoachViewModel
                      {
                          Name = coach.Name,
                          ClientsCount = avg.ClientsCount,
                          Rating = avg.AverageRate
                      })
                .ToList();

            return topCoaches;
        }


    }



}
