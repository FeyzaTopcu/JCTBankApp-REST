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

namespace JCTBank.MobilApp.Controllers
{
    public class HomeController : ApiController
    {
        ICustomerService _customerService = new CustomerManager(new EfRepositoryCustomerDal());

        [HttpPost]
        public IHttpActionResult Login([FromBody]string TCKN, string Password)
        {

            var _customer = _customerService.Login(TCKN, Password);
            if (_customer != null)
            {
                FormsAuthentication.SetAuthCookie(_customer.TCKN, false);

                return Ok(_customer);
            }
            else
            {
                return BadRequest("Giriş gercekleştirilemedi.");
            }

        }

        [HttpPost]
        public IHttpActionResult Register([FromBody]Customer model)
        {

            var _tckn = _customerService.GetByTCKN(model.TCKN);
            if (_tckn != null)
            {
                Random rastgele = new Random();
                model.IsDelete = false;
                model.CustomerNo = rastgele.Next(1, 1000);
                _customerService.Create(model);
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        public IHttpActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return Ok();
        }
    }
}
