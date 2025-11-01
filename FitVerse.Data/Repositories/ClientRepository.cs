using FitVerse.Core.Interfaces;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Core.ViewModels.Profile;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class ClientRepository:GenericRepository<Client>, IClientRepository
    {
        public DbSet<Client> Clients => context.Set<Client>();


        public ClientRepository(FitVerseDbContext context) : base(context)
        {
        }

        public (bool Success,string Message) UpdateClientGoalsRepo(string userId, ClientViewModel clientPhysicalInfo)
        {
            var res= (Success: false, Message: "User Not Found");
            Find(c => c.UserId==userId).ToList().ForEach(client =>
            {
                client.Height = clientPhysicalInfo.Height;
                client.StartWeight = clientPhysicalInfo.StartWeight;
                client.Goal = clientPhysicalInfo.Goal;

                res= (Success : true, Message : "Goals Addes Successfully");
            });
            return res;

        }
        public void DeleteByUserId(string UserId)
        {
            var user = Find(c => c.UserId == UserId).FirstOrDefault();
            if (user != null)
            {
                Clients.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
