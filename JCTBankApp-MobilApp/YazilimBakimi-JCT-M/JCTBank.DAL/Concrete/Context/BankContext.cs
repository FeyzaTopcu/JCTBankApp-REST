using JCTBank.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCTBank.DAL.Context
{
    public class BankContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<HGS> HGSes { get; set; }
        public DbSet<HGSTransaction> HGSTransactions { get; set; }

        public BankContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
