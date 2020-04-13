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
    [Table("Credit")]
    public class Credit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int LoanAmount { get; set; }

        public int CustomerId { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
