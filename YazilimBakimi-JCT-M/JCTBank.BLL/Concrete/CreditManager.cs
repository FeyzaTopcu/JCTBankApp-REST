using JCTBank.BLL.Abstract;
using JCTBank.DAL.Abstract;
using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.BLL.Concrete
{
    public class CreditManager : ICreditService
    {
        private ICreditDal _CreditDal;

        public CreditManager(ICreditDal CreditDal)
        {
            _CreditDal = CreditDal;
        }

        public void Create(Credit entity)
        {
            _CreditDal.Create(entity);
        }

        public void Delete(Credit entity)
        {
            _CreditDal.Update(entity);
        }

        public List<Credit> GetAll(Expression<Func<Credit, bool>> filter = null)
        {
            return _CreditDal.GetAll().ToList();
        }

        public Credit GetById(int id)
        {
            return _CreditDal.GetById(id);
        }

        public void Update(Credit entity)
        {
            _CreditDal.Update(entity);
        }
    }
}
