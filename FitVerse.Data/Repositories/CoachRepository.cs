using AutoMapper;
using FitVerse.Core.Interfaces;
using FitVerse.Core.Models;
using FitVerse.Core.ViewModels.Client;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitVerse.Data.Repositories
{
    public class CoachRepository : GenericRepository<Coach>, ICoachRepository
    {
        public DbSet<Coach> Coaches => context.Set<Coach>();
        public DbSet<Client> Clients => context.Set<Client>();
        public DbSet<Exercise> Exercises => context.Set<Exercise>();
        public DbSet<ExercisePlan> ExercisePlans => context.Set<ExercisePlan>();
        public DbSet<ClientSubscription> ClientSubscriptions => context.Set<ClientSubscription>();
        public DbSet<Payment> Payments => context.Set<Payment>();

        public CoachRepository(Context.FitVerseDbContext context) : base(context) { }

        public Coach GetCoachByIdGuid(string id)
        {
            return Coaches.FirstOrDefault(c => c.Id == id);
        }

        (bool Success, string Message) ICoachRepository.DeleteCoachById(string id)
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

        public double GetAverageRating(string coachId)
        {
            var avg = Coaches
                .Include(c => c.CoachFeedbacks)
                .Where(c => c.Id == coachId)
                .SelectMany(c => c.CoachFeedbacks)
                .Select(f => (double?)f.Rate)
                .Average();

            return avg ?? 0.0;
        }

        // ✅ تعديل لاستخدام ClientSubscription بدل Payments
        public List<ClientDashVM> GetRecentClients(string coachId)
        {
            var recentSubs = ClientSubscriptions
                .Include(cs => cs.Client)
                .Where(cs => cs.CoachId == coachId)
                .OrderByDescending(cs => cs.StartDate)
                .Take(10)
                .ToList();

            var recentClients = recentSubs.Select(cs => new ClientDashVM
            {
                Name = cs.Client.Name,
                IsActive = cs.Client.IsActive,
                LastPaymentAgo = GetTimeAgo(cs.StartDate)
            }).ToList();

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

        public int GetTotalPlans(string coachId)
        {
            return ExercisePlans.Count(ep => ep.CoachId==coachId);
        }

        // ✅ تعديل لاستخدام ClientSubscription بدل Client.CoachId
        public List<ClientDashVM> GetAllClientsByCoach(string coachId)
        {
            var clientsList = ClientSubscriptions
                .Include(cs => cs.Client)
                .Where(cs => cs.CoachId == coachId)
                .Select(cs => cs.Client)
                .Distinct()
                .ToList();

            var clientVMs = clientsList.Select(c => new ClientDashVM
            {
                Name = c.Name,
                IsActive = c.IsActive,
                LastPaymentAgo = c.ClientSubscriptions
                    .Where(cs => cs.CoachId == coachId)
                    .OrderByDescending(cs => cs.StartDate)
                    .Select(cs => GetTimeAgo(cs.StartDate))
                    .FirstOrDefault() ?? "No subscription"
            }).ToList();

            return clientVMs;
        }

      
    }
}
