using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JCTBank.BLL.Abstract;
using JCTBank.BLL.Concrete;
using JCTBank.DAL.Concrete.EfRepository;
using JCTBank.Entity;

namespace JCTBank.MobilApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {

        IAccountService _accountService = new AccountManager(new EfRepositoryAccountDal());
        ICustomerService _customerService = new CustomerManager(new EfRepositoryCustomerDal());

        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody]int CustomerId, Account model)
        {
            var result = _customerService.GetById(CustomerId);

            var accounts = _accountService.GetAccountNo().Where(x => x.AccountNo == result.CustomerNo).OrderBy(x => x.AdditionalAccountNo);
            if (accounts == null)
            {
                model.IsDelete = false;
                model.AdditionalAccountNo = 1001;
                model.AccountNo = result.CustomerNo;
                model.Balance = 0;
                model.CustomerId = result.Id;
                _accountService.Create(model);
                return Ok("İşlem Başarıyla gerçekleşti");
            }
            else
            {
                model.IsDelete = false;
                model.AdditionalAccountNo = 1001 + accounts.Count();
                model.AccountNo = result.CustomerNo;
                model.Balance = 0;
                model.CustomerId = result.Id;
                _accountService.Create(model);
                return Ok("İşlem Başarıyla gerçekleşti");
            }

        }

        [HttpDelete]
        public IHttpActionResult DeleteAccount(int CustomerId, Account model)
        {
            var result = _customerService.GetById(CustomerId);
            var account = _accountService.GetById(model.Id);

            if (account != null)
            {
                if (account.Balance == 0)
                {
                    model.IsDelete = true;
                    model.AccountNo = result.CustomerNo;
                    model.CustomerId = result.Id;
                    _accountService.Delete(model);
                    return Ok("Hesap Silindi");
                }
                else
                {
                    return BadRequest("İşlemin gerçekleşmesi için bakiyenizi sıfırlayınız.");
                }
            }

            else
            {
                return BadRequest("Silinecek hesap seçilemedi");
            }
        }

        [HttpGet]
        public IHttpActionResult ListAccount(int CustomerId)
        {
            var result = _customerService.GetById(CustomerId);
            if (result != null)
            {
                var listAccounts = _accountService.GetAll().Where(x => x.IsDelete == false && x.AccountNo == result.CustomerNo).ToList();
                return Ok(listAccounts);
            }
            else
            {
                return BadRequest("Hesaplar bulunamadı..");
            }

        }


        [HttpPost]

        public IHttpActionResult WithdrawMoney([FromBody]int CustomerId, Account model)
        {
            if (model != null)
            {
                var result = _customerService.GetById(CustomerId);
                var _account = _accountService.GetById(model.Id);

                if (_account != null)
                {
                    if (model.Balance < 0)
                    {
                        return BadRequest("Hesap Bakiyeniz 0'dan az olamaz yeni bir tutar giriniz!");

                    }
                    else
                    {
                        if (_account.Balance >= model.Balance)
                        {
                            _account.Balance = (_account.Balance - model.Balance);
                            _accountService.Update(_account);
                            return Ok("İşlem Başarıyla gerçekleşti");
                        }
                        else
                        {
                            return BadRequest("Hesap Bakiyesi yetersiz..");
                        }
                    }
                }
                else
                {
                    return BadRequest("Böyle bir hesap bulunamadı.");
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IHttpActionResult DepositMoney([FromBody]int CustomerId, Account model)
        {
            if (model != null)
            {
                var result = _customerService.GetById(CustomerId);
                var _account = _accountService.GetById(model.Id);
                if (_account != null)
                {
                    if (model.Balance < 0)
                    {
                        return BadRequest("Hesap Bakiyeniz 0'dan az olamaz yeni bir tutar giriniz!");
                    }

                    else
                    {
                        _account.Balance = (_account.Balance + model.Balance);
                        _accountService.Update(_account);
                        return Ok("İşlem Başarıyla gerçekleşti");
                    }

                }
                else
                {
                    return BadRequest("Böyle bir hesap bulunamadı.");
                }

            }
            else
            {
                return NotFound();
            }
        }
    }
}
