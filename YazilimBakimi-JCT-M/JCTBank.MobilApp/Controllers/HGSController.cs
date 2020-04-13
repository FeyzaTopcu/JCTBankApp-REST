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
    [RoutePrefix("api/hgs")]
    public class HGSController : ApiController
    {
        ICustomerService _customerService = new CustomerManager(new EfRepositoryCustomerDal());
        IHGSService _HGSService = new HGSManager(new EfRepositoryHGSDal());

        [HttpPost]
        [Route("{CustomerId}/sale")]
        public IHttpActionResult SaleHGS(int CustomerId, [FromBody] HGS model)
        {
            bool confirmation;
            if (model.PlateNumber != null)
            {
                var _plate = _HGSService.GetByPlateNumber(model.PlateNumber);
                if (_plate == null)
                {

                    var result = _customerService.GetById(CustomerId);

                    _HGSService.Create(new HGS()
                    {
                        CustomerId = result.Id,
                        CustomerTCKN = result.TCKN,
                        PlateNumber = model.PlateNumber,
                        Balance = 0,
                        WebOrMobil = "Mobil",
                        SaleDate = DateTime.Now
                    });


                    model.SaleDate = DateTime.Now;
                    model.WebOrMobil = "Mobil";

                    using (var hgsSoapClient = new JCTBank.MobilApp.HGSService.HGSServiceSoapClient())
                    {
                        confirmation = hgsSoapClient.SaleHGS(model.Balance, result.Id, result.TCKN, model.PlateNumber, model.SaleDate, model.WebOrMobil);
                    }

                    if (confirmation)
                    {

                        return Ok("İşlem Başarıyla Gerçekleşti");
                    }

                    return Ok("İşlem Başarıyla Gerçekleşti");
                }
                else
                {
                    return BadRequest("Aynı Araç Plaka Numarasına HGS Satın Alınamaz..");

                }

            }

            else
            {
                return BadRequest("Eksik Bilgi Girildi.. Tekrar Deneyiniz..");

            }

        }

        [HttpGet]
        [Route("{CustomerId}")]
        public IHttpActionResult ListSale(int CustomerId)
        {
            var result = _customerService.GetById(CustomerId);
            if (result != null)
            {
                var listSales = _HGSService.GetAll().Where(x => x.CustomerTCKN == result.TCKN).ToList();
                return Ok(listSales);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        [Route("balance")]
        public IHttpActionResult LoadBalance([FromBody]HGS model)
        {
            if (model != null)
            {
                if (model.Balance > 0)
                {
                    var result = _HGSService.GetById(model.Id);
                    result.Balance = result.Balance + model.Balance;

                    _HGSService.Update(new HGS()
                    {
                        Id = result.Id,
                        CustomerId = result.CustomerId,
                        CustomerTCKN = result.CustomerTCKN,
                        Balance = result.Balance,
                        PlateNumber = result.PlateNumber,
                        WebOrMobil = "Mobil",
                        SaleDate = DateTime.Now
                    });

                    using (var hgsSoapClient = new JCTBank.MobilApp.HGSService.HGSServiceSoapClient())
                    {
                        hgsSoapClient.LoadBalance(result.PlateNumber, result.Balance);
                    }

                    return Ok("İşlem Başarıyla Gerçekleşti");
                }

                else
                {
                    return BadRequest("Eksik Bilgi Girildi.. Tekrar Deneyiniz..");

                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{id}/balance")]
        public IHttpActionResult UpdateBalance(int id)
        {
            var model = _HGSService.GetById(id);
            if (model == null)
            {
                throw new Exception(" Hesap Seçilmedi..");
            }

            using (var hgsSoapClient = new JCTBank.MobilApp.HGSService.HGSServiceSoapClient())
            {
                hgsSoapClient.UpdateBalance(model.PlateNumber);

            }

            return Ok("İşlem Başarıyla Gerçekleşti");
        }
    }
}
