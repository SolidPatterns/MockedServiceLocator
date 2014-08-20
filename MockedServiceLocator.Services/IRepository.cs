using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MockedServiceLocator.Services
{
    public interface IRepository<T>
    {
        IQueryable<T> Queryable { get; }
        void Create(T entity);
        void Delete(T entity);
        T Find(object id);
        void SaveChanges();
        IEnumerable<T> ToList();
        void Update(T entity);
        IEnumerable<T> Where(Expression<Func<T, bool>> filter);
    }
}