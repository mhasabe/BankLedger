using BankingLedger.Core;
using BankingLedger.Core.Interfaces;
using BankingLedger.Data;
using BankingLedger.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            UserSelection(serviceProvider);

        }

        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<AccountContext>(x => x.UseInMemoryDatabase("InMemoryDatabase"));
            serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
        }

        static public void UserSelection(ServiceProvider provider)
        {
            try
            {
                int userInput = 0;
                string userName = string.Empty;
                string password = string.Empty;

                var accountRepo = provider.GetService<IAccountRepository>();

                do
                {
                    userInput = MainMenu();

                    switch (userInput.ToString())
                    {
                        case "1":
                            try { 
                            Console.WriteLine("Enter UserName");
                            userName = Console.ReadLine();
                            Console.WriteLine("Enter Password");
                            password = Console.ReadLine();
                            accountRepo.CreateAccount(userName, password);
                            Console.WriteLine("Success: Account Created Succesfully");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error : {ex.Message}");
                            }
                            break;

                        case "2":
                            try { 
                            Console.WriteLine("Enter UserName");
                            userName = Console.ReadLine();
                            Console.WriteLine("Enter Password");
                            password = Console.ReadLine();
                            var account = accountRepo.GetAccount(userName, password);
                            UserSubMenuSelection(account.AccountId, accountRepo);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error : {ex.Message}");
                            }
                            break;

                    }
                } while (userInput != 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        static void UserSubMenuSelection(int accountID, IAccountRepository repo)
        {
            int userSubMenuInput = 0;

            do
            {
                userSubMenuInput = SubMenu();


                switch (userSubMenuInput.ToString())
                {
                    case "1":
                        var acc = repo.GetAccountWithLedger(accountID);

                        if (acc.Ledger.Count > 0)
                        {
                            Console.WriteLine(String.Format("|{0,15}|{1,25}|{2,15}", "Amount", "Date Time", "Transaction Type"));

                            foreach (var ledger in acc.Ledger)
                            {
                                Console.WriteLine(String.Format("|{0,15}|{1,25}|{2,15}", ledger.Amount, ledger.TimeStamp, ledger.TransactionType));
                            }
                        }
                        

                        break;
                    case "2":
                        try
                        {
                            Console.WriteLine("Enter Amount to Deposit");
                            decimal amountDeposit = Convert.ToDecimal(Console.ReadLine());
                            var accDepo = repo.GetAccountWithLedger(accountID);
                            accDepo.Deposit(amountDeposit);
                            repo.UpdateAccount(accDepo);
                            Console.WriteLine("Success: Amount Deposited Succesfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error : {ex.Message}");
                        }
                        break;
                    case "3":
                        try
                        {
                            Console.WriteLine("Enter Amount to withdraw");
                            decimal amountWithDraw = Convert.ToDecimal(Console.ReadLine());
                            var accWith = repo.GetAccountWithLedger(accountID);
                            accWith.Withdraw(amountWithDraw);
                            repo.UpdateAccount(accWith);
                            Console.WriteLine("Success: Amount Withdraw Succesfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error : {ex.Message}");
                        }
                        break;
                    case "4":
                        
                            try
                            {
                            var accBalance = repo.checkBalance(accountID);
                            Console.WriteLine($"Account Balance:  {accBalance }" );
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error : {ex.Message}");
                            }
                            break;                        
                }

            } while (userSubMenuInput != 5);

        }


        static public int SubMenu()
        {

            Console.WriteLine("*****************************************************************************");
            Console.WriteLine();
            Console.WriteLine("                    Press 1. ==>  Transaction History");
            Console.WriteLine("                    Press 2. ==>  Deposit");
            Console.WriteLine("                    Press 3. ==>  Withdraw");
            Console.WriteLine("                    Press 4. ==>  Account Balance");
            Console.WriteLine("                    Press 5. ==>  Logout");
            Console.WriteLine();
            Console.WriteLine("*****************************************************************************");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        static public int MainMenu()
        {
            Console.WriteLine("*******************************Banking XYZ***********************************");
            Console.WriteLine();
            Console.WriteLine("                        Press 1. ==> Create a New Account                    ");
            Console.WriteLine("                        Press 2. ==> Login                                   ");
            Console.WriteLine("                        Press 3. ==> Exit                                    ");
            Console.WriteLine();
            Console.WriteLine("*****************************************************************************");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }


    }




}
