using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NETCoreApp.Models;

namespace NETCoreApp.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
    }
}