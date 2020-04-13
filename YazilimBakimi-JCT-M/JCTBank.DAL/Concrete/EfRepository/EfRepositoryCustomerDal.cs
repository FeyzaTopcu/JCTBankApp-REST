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
    public class EfRepositoryCustomerDal : EfGenericRepository<Customer, BankContext>, ICustomerDal
    {
        BankContext context = new BankContext();
        public Customer Login(string TCKN, string Password)
        {
            return context.Customers.Where(x => x.TCKN == TCKN && x.Password == Password).SingleOrDefault();
        }
        public Customer GetByTCKN(string TCKN)
        {
            return context.Customers.Where(x => x.TCKN == TCKN ).SingleOrDefault();
        }

        public Customer GetByNo(int customerNo)
        {
            return context.Customers.Where(x => x.CustomerNo == customerNo).SingleOrDefault();
        }
    }
}
