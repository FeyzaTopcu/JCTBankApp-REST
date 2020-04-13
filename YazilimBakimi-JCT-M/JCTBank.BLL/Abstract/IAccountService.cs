using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.BLL.Abstract
{
    public interface IAccountService : IGenericService<Account>
    {
        Account GetByCustomerNo(int CustomerNo, int additionalNo);
        List<Account> GetAccountNo();
    }
}
