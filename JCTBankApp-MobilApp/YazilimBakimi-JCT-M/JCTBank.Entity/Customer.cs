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
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Customer No")]
        public int CustomerNo { get; set; }

        [DisplayName("TCKN"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            MinLength(11, ErrorMessage = "{0} alanı {1} karakter olmalıdır."),
            MaxLength(11, ErrorMessage = "{0} alanı {1} karakter olmalıdır.")]
        public string TCKN { get; set; }

        [DisplayName("Password"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            DataType(DataType.Password),
            MinLength(8, ErrorMessage = "{0} alanı min. {1} karakter olmalıdır."),
            MaxLength(16, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [DisplayName("RePassword"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            Compare("Password")]
        public string RePassword { get; set; }

        [DisplayName("Email"),
            DataType(DataType.EmailAddress),
            Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Email { get; set; }

        [DisplayName("Name"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Surname"),
            Required(ErrorMessage = "{0} alanı gereklidir."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Address"),
           Required(ErrorMessage = "{0} alanı gereklidir.")]
        public string Address { get; set; }

        [DisplayName("Phone Number"),
           Required(ErrorMessage = "{0} alanı gereklidir."),
           DataType(DataType.PhoneNumber)]
        public int PhoneNo { get; set; }

        public bool IsDelete { get; set; }

        /*public virtual Account Account { get; set; }
        public virtual Transfer Transfer { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual HGS HGS { get; set; }*/

        public virtual ICollection<Account> Accounts { get; set; }

    }
}
