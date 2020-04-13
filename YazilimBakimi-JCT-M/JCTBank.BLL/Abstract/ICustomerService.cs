using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.BLL.Abstract
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Customer Login(string TCKN, string Password);
        Customer GetByTCKN(string TCKN);

        Customer GetByNo(int customerNo);
    }
}
