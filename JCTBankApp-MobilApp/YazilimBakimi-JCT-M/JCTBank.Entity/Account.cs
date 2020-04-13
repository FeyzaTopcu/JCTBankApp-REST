using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.Entity
{
    [Table("Account")]
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AccountNo { get; set; }
        public int AdditionalAccountNo { get; set; }
        public decimal Balance { get; set; }
        public bool IsDelete { get; set; }


        public int CustomerId { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
