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
    [Table("HGS")]
    public class HGS
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerTCKN { get; set; }
        public string PlateNumber { get; set; }
        public decimal Balance { get; set; }
        public string WebOrMobil { get; set; }
        public DateTime SaleDate { get; set; }

        public int CustomerId { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }

    }
}
