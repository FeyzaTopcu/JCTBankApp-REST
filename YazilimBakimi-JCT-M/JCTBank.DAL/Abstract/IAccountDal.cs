using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.DAL.Abstract
{
    public interface IAccountDal : IRepository<Account>
    {
        Account GetByCustomerNo(int CustomerNo, int additionalNo);
        List<Account> GetAccountNo();
    }
}
