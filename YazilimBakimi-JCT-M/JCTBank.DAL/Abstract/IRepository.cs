using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.DAL.Abstract
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        //IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        //T GetOne(Expression<Func<T, bool>> filter);
    }
}
