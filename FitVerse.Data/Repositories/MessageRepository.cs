using FitVerse.Core.Interfaces;
using FitVerse.Data.Context;
using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly FitVerseDbContext _context;

        public MessageRepository(FitVerseDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
