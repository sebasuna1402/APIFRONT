using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGenericSv<T> where T:class
    {
        //Commands
        public T Add(T candidate);
        public T Update(T candidate);
        public void Delete(int id);
        public void Delete(params object[] ids);

        //Queries
        public List<T> GetAll(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        public T GetById(int id);
        public T GetByCondition(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
    }
}
