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
    public class HGSTransactionManager : IHGSTransactionService
    {
        private IHGSTransactionDal _HGSTransactionDal;

        public HGSTransactionManager(IHGSTransactionDal HGSTransactionDal)
        {
            _HGSTransactionDal = HGSTransactionDal;
        }

        public void Create(HGSTransaction entity)
        {
            _HGSTransactionDal.Create(entity);
        }

        public void Delete(HGSTransaction entity)
        {
            _HGSTransactionDal.Update(entity);
        }

        public List<HGSTransaction> GetAll(Expression<Func<HGSTransaction, bool>> filter = null)
        {
            return _HGSTransactionDal.GetAll().ToList();
        }

        public HGSTransaction GetById(int id)
        {
            return _HGSTransactionDal.GetById(id);
        }

        public void Update(HGSTransaction entity)
        {
            _HGSTransactionDal.Update(entity);
        }
    }
}
