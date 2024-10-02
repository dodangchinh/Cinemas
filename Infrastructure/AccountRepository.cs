using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chinh_C1_Cinemas
{
    public class AccountRepository
    {
        public List<Account> lstAccount { get; set; }

        public AccountRepository() 
        {
            lstAccount = new List<Account>();
            Load();
        }

        public void Load()
        {
            DataProvider.pathData = "data/Account.xml";
            DataProvider.Open();

            string xPath = string.Format("//Account");

            XmlNodeList listNode = DataProvider.getDsNode(xPath);

            foreach (XmlNode item in listNode)
            {
                Account account = new Account();
                account.Name = item.Attributes["Name"].Value;
                account.Username = item.Attributes["Username"].Value;
                account.Password = item.Attributes["Password"].Value;
                account.Role = item.Attributes["Role"].Value;
                lstAccount.Add(account);
            }
            DataProvider.Close();
        }
    }
}
