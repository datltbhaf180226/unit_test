using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NETCoreApp.Data;
using NETCoreApp.Models;

namespace NETCoreApp.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyDbContext Context;
        protected readonly DbSet<T> Entities;

        public GenericRepository(MyDbContext context)
        {
            this.Context = context;
            Entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Entities.AsQueryable();
            return includeProperties.Aggregate(query, (current, include) => current.Include(include));
        }

        public T Get(long id)
        {
            return Entities.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entities.Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entities.Remove(entity);
            Context.SaveChanges();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Entities.AsQueryable();
            query = query.Where(predicate);
            query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            var temp = query.ToQueryString();
            return query;
        }
    }
}