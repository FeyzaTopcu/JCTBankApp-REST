using JCTBank.BLL.Abstract;
using JCTBank.BLL.Concrete;
using JCTBank.DAL.Concrete.EfRepository;
using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace JCTBankMobilApp.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        ICustomerService _customerService = new CustomerManager(new EfRepositoryCustomerDal());

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult ShowProfile(int id)
        {
            var result = _customerService.GetById(id);
            if (result != null)
            {
                var profileCustomer = _customerService.GetAll().Where(x => x.CustomerNo == result.CustomerNo).ToList();
                return Ok(profileCustomer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateCustomer(int id, [FromBody]Customer model)
        {
            var result = _customerService.GetById(id);
            if (result != null)
            {
                model.TCKN = result.TCKN;
                model.CustomerNo = result.CustomerNo;
                result.Name = model.Name;
                result.Surname = model.Surname;
                result.TCKN = model.TCKN;
                result.CustomerNo = model.CustomerNo;
                result.IsDelete = false;
                result.Id = result.Id;
                result.PhoneNo = model.PhoneNo;
                result.Address = model.Address;
                result.Email = model.Email;
                result.Password = model.Password;
                result.RePassword = model.RePassword;
                _customerService.Update(model);
                return Ok("Bilgiler güncellendi");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
