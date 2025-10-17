using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class CoachRepository : GenericRepository<Data.Models.Coach>, FitVerse.Core.Interfaces.ICoachRepository
    {
        public DbSet<Coach> Coaches => context.Set<Coach>();

        public CoachRepository(Context.FitVerseDbContext context) : base(context)
        {
        }
        public Coach GetCoachByIdGuid(Guid id) {
            var coach =Coaches.FirstOrDefault(c => c.Id == id); 
            if (coach == null)
                return null;
            return coach;

        }



        (bool Success, string Message) ICoachRepository.DeleteCoachById(Guid id)
        {
            try
            {
                var coach = Coaches.FirstOrDefault(c => c.Id == id);
                if (coach == null)
                    return (false, "Coach not found.");
                Coaches.Remove(coach);
                context.SaveChanges();
                return (true, "Coach deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }




    }

}
