using BankingLedger.Core.Model;
using System;
using System.Collections.Generic;

namespace BankingLedger.Core
{
    public class Account
    {
        
        private Account()
        {
            Ledger = new List<Ledger>();
        }

        public int AccountId { get; set; }
        public decimal Balance { get; private set; }
        public string UserName { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public List<Ledger> Ledger { get; private set; }



        public void Deposit(decimal money)
        {
            Ledger.Add(new Model.Ledger {AccountId=AccountId,
                TimeStamp =DateTime.Now,TransactionType=TransactionType.Deposit,Amount=money });
            Balance += money;
        }

        public void Withdraw(decimal money)
        {
            if (!canWithdraw(money))
            {
                throw new Exception("Insufficient Balance");
            }

            Ledger.Add(new Model.Ledger
            {
                AccountId = AccountId,
                TimeStamp = DateTime.Now,
                TransactionType = TransactionType.Withdraw,
                Amount = money
            });

            Balance -= money;
        }

        public bool canWithdraw(decimal money)
        {
            return Balance >= money ? true : false;
        }

        public static Account Create(string userName,string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            //if (_context.Accounts.Any(x => x.UserName == userName))
            //    throw new Exception("Username \"" + userName + "\" is already taken");


            var account = new Account();

            byte[] passwordHash, passwordSalt;
            Helper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            account.UserName = userName;
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;


            return account;
        }

    }
}
