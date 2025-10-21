using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.UnitOfWork;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Models;
using FitVerse.Data.UnitOfWork;
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
        public DbSet<Client> Clients => context.Set<Client>();
        public DbSet<Exercise> Exercises => context.Set<Exercise>();
        public DbSet<ExercisePlan> ExercisePlans => context.Set<ExercisePlan>();
        public DbSet<Payment> Payments => context.Set<Payment>();


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

        public int GetActiveClientsCount()
        {
            return Clients.Count(c => c.IsActive);
        }

    

        public int GetTotalExercises()
        {
            return Exercises.Count();
        }


        public double GetAverageRating(Guid coachId)
        {
            var avg = Coaches
                .Include(c => c.CoachFeedbacks)
                .Where(c => c.Id == coachId)
                .SelectMany(c => c.CoachFeedbacks)
                .Select(f => (double?)f.Rate)
                .Average();

            return avg ?? 0.0;
        }
        public List<ClientDashVM> GetRecentClients(Guid coachId)
        {
            var clientJoinDates = Payments
                .Where(p => p.Package.CoachId == coachId)
                .GroupBy(p => p.ClientId)
                .Select(g => new
                {
                    ClientId = g.Key,
                    JoinDate = g.Max(p => p.PaymentDate)
                })
            .ToList();

            var recentClients = Clients
                .AsEnumerable()
                .Join(clientJoinDates,
                      c => c.Id,
                      j => j.ClientId,
                      (c, j) => new
                      {
                          Client = c,
                          JoinDate = j.JoinDate
                      })
                .OrderByDescending(x => x.JoinDate)
                .Select(x => new ClientDashVM
                {

                    Name = x.Client.Name,
                    IsActive = x.Client.IsActive,
                    LastPaymentAgo = GetTimeAgo(x.JoinDate) // 👈 نحسب الوقت هنا
                })
                .ToList();
        
           return recentClients;
        }
        
           private static string GetTimeAgo(DateTime date)
        {
            var diff = DateTime.Now - date;

            if (diff.TotalDays >= 1)
                return $"{(int)diff.TotalDays} day{(diff.TotalDays >= 2 ? "s" : "")} ago";
            if (diff.TotalHours >= 1)
                return $"{(int)diff.TotalHours} hour{(diff.TotalHours >= 2 ? "s" : "")} ago";
            if (diff.TotalMinutes >= 1)
                return $"{(int)diff.TotalMinutes} minute{(diff.TotalMinutes >= 2 ? "s" : "")} ago";

            return "Just now";
        }
        

        public int GetTotalPlans(Guid coachId)
        {
            Console.WriteLine("CoachId Received: " + coachId);
            var count = ExercisePlans.Count(ep => ep.CoachId == coachId);
            Console.WriteLine("Plans Found: " + count);
            return count;
        }
        public List<ClientDashVM> GetAllClientsByCoach(Guid coachId)
        {
            var clientsList = Clients
             .Where(c => c.CoachId == coachId)
             .Include(c => c.Payments)
             //.AsNoTracking()
             .ToList(); // تنفيذ الاستعلام

            var clientVMs = clientsList.Select(c => new ClientDashVM
            {
                Name = c.Name,
               
                IsActive = c.IsActive,
                LastPaymentAgo = c.Payments.Any()
                    ? GetTimeAgo(c.Payments.Max(p => p.PaymentDate))
                    : "No payment"
            }).ToList();

            return clientVMs;
        }



    }

}
