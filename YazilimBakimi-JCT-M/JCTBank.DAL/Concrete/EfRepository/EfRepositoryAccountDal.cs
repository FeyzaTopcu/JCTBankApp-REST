using JCTBank.DAL.Abstract;
using JCTBank.DAL.Context;
using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.DAL.Concrete.EfRepository
{
    public class EfRepositoryAccountDal : EfGenericRepository<Account, BankContext>, IAccountDal
    {
        BankContext context = new BankContext();

        public List<Account> GetAccountNo()
        {
            return context.Accounts.AsNoTracking().ToList();
        }

        public Account GetByCustomerNo(int CustomerNo , int additionalNo)
        {
            return context.Accounts.Where(x => x.AccountNo == CustomerNo && x.AdditionalAccountNo == additionalNo).SingleOrDefault()/*.OrderBy(x=>x.AdditionalAccountNo)*/;
        }
    }
}
