using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core.Model
{
    public class Ledger
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
