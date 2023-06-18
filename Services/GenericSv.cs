using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services
{
    public class GenericSv<T> : IGenericSv<T> where T:class
    {
        public readonly MyDbContext _myDbContext;
        private readonly DbSet<T> _DbSet;
        public GenericSv(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
            _DbSet = _myDbContext.Set<T>();
        }

        //Commands
        public T Add(T entity)
        {
            _myDbContext.Add(entity);
            _myDbContext.SaveChanges();

            return entity;
        }
        public T Update(T entity)
        {
            _DbSet.Attach(entity);
            _myDbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public void Delete(int id)
        {
            var entity = _DbSet.Find(id);

            if (entity is not null)
            {
                _DbSet.Remove(entity);
                _myDbContext.SaveChanges();
            }
                
        }

        public void Delete(params object[] ids)
        {
            var entity = _DbSet.Find(ids);

            if (entity is not null)
            {
                _DbSet.Remove(entity);
                _myDbContext.SaveChanges();
            }                

        }


        //Queries

        public List<T> GetAll(Expression<Func<T, bool>>? filter = null, string includeProperties = "") 
        {
            IQueryable<T> query = _DbSet;

            if(filter is not null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public T GetByCondition(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _DbSet;

            if (filter is not null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList().FirstOrDefault();
        }

        public T GetById(int id)
        {
            return _DbSet.Find(id);
        }

    }
}
