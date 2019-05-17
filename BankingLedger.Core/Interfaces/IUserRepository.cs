using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger.Core.Interfaces
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
    }
}
