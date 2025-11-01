using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        IQueryable<T> GetQueryable();


        void Delete(T entity); 
        void RemoveRange(IEnumerable<T> entities);
        void complete();

    }
}
