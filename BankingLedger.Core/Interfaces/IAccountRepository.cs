using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccount(string userName, string password);
        Account CreateAccount(string userName,string password);
        Account GetAccountWithLedger(int accountId);
        Account GetAccount(int accountId);
        decimal checkBalance(int accountId);
        void UpdateAccount(Account account); 
    }
}
