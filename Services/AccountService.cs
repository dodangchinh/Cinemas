using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class AccountService
    { 
        public bool isExit(Account account)
        {
            foreach (var item in UnitOfWork.Instance.accountRepository.lstAccount)
            {
                if (item.Username.CompareTo(account.Username) == 0
                    && item.Password.CompareTo(account.Password) == 0)
                    return true;
            }

            return false;
        }

        public Account getAccount(string username, string password)
        {
            foreach (var item in UnitOfWork.Instance.accountRepository.lstAccount)
            {
                if (item.Username == username && item.Password == password)
                    return new Account(item.Name, item.Username, item.Password, item.Role);
            }
            return null;
        }
    }
}
