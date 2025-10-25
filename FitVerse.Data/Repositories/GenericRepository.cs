using FitVerse.Core.Interfaces;
using FitVerse.Core.ViewModels.Equipment;
using FitVerse.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FitVerseDbContext context;
        protected readonly DbSet<T> dbSet;
        public GenericRepository(FitVerseDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public T Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Add(entity);
            return entity;

        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            dbSet.Remove(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();

        }
        public IEnumerable<T> GetAll(params string[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }


        public T GetById(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return dbSet.Find(id);
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            dbSet.Update(entity);
        }



    }
}
