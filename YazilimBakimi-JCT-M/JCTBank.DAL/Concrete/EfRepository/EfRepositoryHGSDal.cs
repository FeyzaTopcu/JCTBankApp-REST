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
    public class EfRepositoryHGSDal : EfGenericRepository<HGS, BankContext>, IHGSDal
    {
        BankContext context = new BankContext();
        public HGS GetByPlateNumber(string plateNumber)
        {
            return context.HGSes.Where(x => x.PlateNumber == plateNumber).SingleOrDefault();
        }
    }
}
