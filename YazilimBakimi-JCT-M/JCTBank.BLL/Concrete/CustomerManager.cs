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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _CustomerDal;

        public CustomerManager(ICustomerDal CustomerDal)
        {
            _CustomerDal = CustomerDal;
        }

        public void Create(Customer entity)
        {
            _CustomerDal.Create(entity);
        }

        public void Delete(Customer entity)
        {
            _CustomerDal.Update(entity);
        }

        public List<Customer> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            return _CustomerDal.GetAll().ToList();
        }

        public Customer GetById(int id)
        {
            return _CustomerDal.GetById(id);
        }

        public Customer GetByNo(int customerNo)
        {
            return _CustomerDal.GetByNo(customerNo);
        }

        public Customer GetByTCKN(string TCKN)
        {
            return _CustomerDal.GetByTCKN(TCKN);
        }

        public Customer Login(string TCKN, string Password)
        {
            var customer = _CustomerDal.Login(TCKN, Password);
            if (customer == null)
                throw new Exception("Kullanıcı Adı veya Parola Hatalı. ");
            else
                return customer;
        }

        public void Update(Customer entity)
        {
            _CustomerDal.Update(entity);
        }
    }
}
