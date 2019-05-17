using BankingLedger.Core;
using BankingLedger.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Data
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> dbContext):base(dbContext)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Ledger> Ledger { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
