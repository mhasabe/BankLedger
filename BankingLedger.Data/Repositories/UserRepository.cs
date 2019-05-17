using BankingLedger.Core;
using BankingLedger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AccountContext _context;

        public UserRepository(AccountContext context)
        {
            _context = context;
        }


        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
             
            if (user == null)
                return null;
             
            if (!Helper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
  
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new Exception("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            Helper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

    }
}
