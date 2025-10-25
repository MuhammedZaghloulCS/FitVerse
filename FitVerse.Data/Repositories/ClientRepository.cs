using FitVerse.Core.Interfaces;
using FitVerse.Core.ViewModels.Coach;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
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
      
        public ClientRepository(FitVerseDbContext context) : base(context)
        {
        }

    }
}
