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
    public class TransferController : ApiController
    {

        ITransferService _transferService = new TransferManager(new EfRepositoryTransferDal());
        IAccountService _accountService = new AccountManager(new EfRepositoryAccountDal());
        ICustomerService _customerService = new CustomerManager(new EfRepositoryCustomerDal());

        [HttpGet]
        public IHttpActionResult TransferList(int CustomerId)
        {
            var result = _customerService.GetById(CustomerId);
            if (result != null)
            {
                var listTransfers = _transferService.GetAll().Where(x => x.SendingCustomerNo == result.CustomerNo).ToList();
                return Ok(listTransfers);
            }
            else
            {
                return BadRequest("Transfer listesi bulunaamadı..");
            }

        }

        [HttpGet]
        public IHttpActionResult GetAccount(int CustomerId)
        {
            var result = _customerService.GetById(CustomerId);
            if (result != null)
            {
                var listAccounts = _accountService.GetAll().Where(x => x.IsDelete == false && x.AccountNo == result.CustomerNo).ToList();
                Transfer transferModel = new Transfer { Accounts = listAccounts };

                return Ok(transferModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult MoneyTransfer([FromBody] Transfer model, int sendId)
        {
            if (ModelState.IsValid)
            {
                var sendCustomer = _accountService.GetById(sendId);
                if (sendCustomer != null && sendCustomer.Balance > 0)
                {
                    var receivedCustomer = _customerService.GetByNo(model.ReceiverNo);
                    if (receivedCustomer != null)
                    {
                        var received = _accountService.GetByCustomerNo(model.ReceiverNo, model.AdditionalReceiverNo);
                        if (received != null)
                        {
                            if (sendCustomer.Balance >= model.Balance && model.Balance > 0)
                            {
                                _transferService.Create(new Transfer()
                                {
                                    TransferType = "MoneyTransfer",
                                    WebOrMobil = "Web",
                                    Date = DateTime.Now,
                                    SendingCustomerNo = sendCustomer.AccountNo,
                                    AdditionalSendingCustomerNo = sendCustomer.AdditionalAccountNo,
                                    ReceiverNo = received.AccountNo,
                                    AdditionalReceiverNo = received.AdditionalAccountNo,
                                    Balance = model.Balance
                                });

                                _accountService.Update(new Account()
                                {
                                    Id = sendCustomer.Id,
                                    AccountNo = sendCustomer.AccountNo,
                                    AdditionalAccountNo = sendCustomer.AdditionalAccountNo,
                                    Balance = sendCustomer.Balance - model.Balance,
                                    CustomerId = sendCustomer.CustomerId,
                                    IsDelete = sendCustomer.IsDelete
                                });


                                _accountService.Update(new Account()
                                {
                                    Id = received.Id,
                                    AccountNo = received.AccountNo,
                                    AdditionalAccountNo = received.AdditionalAccountNo,
                                    Balance = received.Balance + model.Balance,
                                    CustomerId = received.CustomerId,
                                    IsDelete = received.IsDelete
                                });

                                return Ok("İşlem Basarıyla gerceklesti");
                            }
                            else
                            {
                                return BadRequest("Bakiye Sıfırın Altında YA DA HESABINIZDA YETERLİ MİKTAR YOK.  Havale Gerçekleştirilemedi..");
                            }
                        }

                        else
                        {
                            return BadRequest("Hesap Numarası Bulunamadı. Lütfen kontrol ediniz..");

                        }
                    }

                    else
                    {
                        return BadRequest("Alan Müşteri Numarası Bulunamadı. Lütfen kontrol ediniz..");

                    }
                }

                else
                {
                    return BadRequest("Hesap Bulunamadı veya Hesaptaki Bakiyeniz 0 ın Altında. Lütfen kontrol ediniz..");

                }
            }

            else
            {
                return BadRequest("Eksik Bilgi Girildi.. Tekrar Deneyiniz..");

            }
        }
        [HttpPost]
        public IHttpActionResult Virement([FromBody] int sendId, int receivedId, Transfer model)
        {
            if (ModelState.IsValid)
            {
                var sendCustomer = _accountService.GetById(sendId);
                var receivedCustomer = _accountService.GetById(receivedId);
                if (sendCustomer != null && sendCustomer.Balance > 0)
                {
                    if (receivedCustomer != null)
                    {
                        if (sendCustomer.AdditionalAccountNo != receivedCustomer.AdditionalAccountNo)
                        {
                            if (sendCustomer.Balance >= model.Balance && model.Balance > 0)
                            {
                                _transferService.Create(new Transfer()
                                {
                                    TransferType = "Virement",
                                    WebOrMobil = "Web",
                                    Date = DateTime.Now,
                                    SendingCustomerNo = sendCustomer.AccountNo,
                                    AdditionalSendingCustomerNo = sendCustomer.AdditionalAccountNo,
                                    ReceiverNo = receivedCustomer.AccountNo,
                                    AdditionalReceiverNo = receivedCustomer.AdditionalAccountNo,
                                    Balance = model.Balance
                                });

                                _accountService.Update(new Account()
                                {
                                    Id = sendCustomer.Id,
                                    AccountNo = sendCustomer.AccountNo,
                                    AdditionalAccountNo = sendCustomer.AdditionalAccountNo,
                                    Balance = sendCustomer.Balance - model.Balance,
                                    CustomerId = sendCustomer.CustomerId,
                                    IsDelete = sendCustomer.IsDelete
                                });


                                _accountService.Update(new Account()
                                {
                                    Id = receivedCustomer.Id,
                                    AccountNo = receivedCustomer.AccountNo,
                                    AdditionalAccountNo = receivedCustomer.AdditionalAccountNo,
                                    Balance = receivedCustomer.Balance + model.Balance,
                                    CustomerId = receivedCustomer.CustomerId,
                                    IsDelete = receivedCustomer.IsDelete
                                });

                                return Ok("İşlem Basarıyla gerceklesti");
                            }
                            else
                            {
                                return BadRequest("Hesabınızda yeterli miktar yok ya da girdiğiniz bakiye 0 ın altında ...");

                            }

                        }

                        else
                        {
                            return BadRequest("Gönderen Hesap Numarasıyla Alan Hesap Numarası Aynı Olamaz..");

                        }
                    }

                    else
                    {
                        return BadRequest("Alan Müşteri Numarası Bulunamadı. Lütfen kontrol ediniz..");

                    }
                }

                else
                {
                    return BadRequest("Hesap Bulunamadı veya Hesaptaki Bakiyeniz 0 ın Altında. Lütfen kontrol ediniz..");

                }
            }

            else
            {
                return BadRequest("Eksik Bilgi Girildi.. Tekrar Deneyiniz..");

            }
        }
    }
}
