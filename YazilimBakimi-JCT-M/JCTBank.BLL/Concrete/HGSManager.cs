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
    public class HGSManager : IHGSService
    {
        private IHGSDal _HGSDal;

        public HGSManager(IHGSDal HGSDal)
        {
            _HGSDal = HGSDal;
        }

        public void Create(HGS entity)
        {
            _HGSDal.Create(entity);
        }

        public void Delete(HGS entity)
        {
            _HGSDal.Update(entity);
        }

        public List<HGS> GetAll(Expression<Func<HGS, bool>> filter = null)
        {
            return _HGSDal.GetAll().ToList();
        }

        public HGS GetById(int id)
        {
            return _HGSDal.GetById(id);
        }
        public HGS GetByPlateNumber(string PlateNumber)
        {
            return _HGSDal.GetByPlateNumber(PlateNumber);
        }

        public void Update(HGS entity)
        {
            _HGSDal.Update(entity);
        }
    }
}
