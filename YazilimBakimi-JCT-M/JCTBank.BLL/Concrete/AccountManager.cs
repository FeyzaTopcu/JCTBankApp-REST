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
    public class AccountManager : IAccountService
    {
        private IAccountDal _AccountDal;

        public AccountManager(IAccountDal AccountDal)
        {
            _AccountDal = AccountDal;
        }

        public void Create(Account entity)
        {
            _AccountDal.Create(entity);
        }

        public void Delete(Account entity)
        {
            _AccountDal.Delete(entity);
        }

        public List<Account> GetAccountNo()
        {
            return _AccountDal.GetAccountNo().ToList();
        }

        public List<Account> GetAll(Expression<Func<Account, bool>> filter = null)
        {
            return _AccountDal.GetAll().ToList();
        }

        public Account GetByCustomerNo(int CustomerNo, int additionalNo)
        {
            return _AccountDal.GetByCustomerNo( CustomerNo, additionalNo);
        }

        public Account GetById(int id)
        {
            return _AccountDal.GetById(id);
        }

        public void Update(Account entity)
        {
            _AccountDal.Update(entity);
        }
    }
}
