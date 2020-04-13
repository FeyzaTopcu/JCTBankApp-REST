using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.Entity
{
    [Table("Transfer")]
    public class Transfer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Gönderen Müşteri Numarası"),
            Required(ErrorMessage = "{0} alanı gereklidir.")]
        public int SendingCustomerNo { get; set; }

        public int AdditionalSendingCustomerNo { get; set; }

        [DisplayName("Alan Müşteri Numarası"),
            Required(ErrorMessage = "{0} alanı gereklidir.")]
        public int ReceiverNo { get; set; }

        public int AdditionalReceiverNo { get; set; }

        public decimal Balance { get; set; }
        public string TransferType { get; set; }
        public string WebOrMobil { get; set; }
        public DateTime Date { get; set; }
        public List<Customer> Customers { get; set; }
        [JsonIgnore]
        public List<Account> Accounts { get; set; }
    }
}
