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
    public class EfRepositoryCreditDal : EfGenericRepository<Credit, BankContext>, ICreditDal
    {
    }
}
