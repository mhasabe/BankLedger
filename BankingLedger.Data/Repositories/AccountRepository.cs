using BankingLedger.Core;
using BankingLedger.Core.Interfaces;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BankingLedger.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _context;

        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        public decimal checkBalance(int accountId)
        {
            return _context.Accounts.Find(accountId).Balance;
        }

        public Account GetAccountWithLedger(int accountId)
        {
            return _context.Accounts.
                Include(x => x.Ledger).
                Where(a => a.AccountId == accountId).
                FirstOrDefault();
        }

        public Account GetAccount(int accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public Account GetAccount(string userName, string password)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.UserName == userName);

            if (account == null)
                throw new Exception("Account Not found");

            if (!Helper.VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
                throw new Exception("Invalid Password");

            return account;
        }

        public void UpdateAccount(Account account)
        {
            _context.SaveChanges();
        }

        public Account CreateAccount(string userName, string password)
        {
            var account = Account.Create(userName, password);
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

      
    }
}
