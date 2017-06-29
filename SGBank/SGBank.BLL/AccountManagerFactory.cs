using SGBank.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL
{
    public static class AccountManagerFactory
    {
        public static AccountManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString(); //this code looks at the app.config and pulls the value from Mode

            switch(mode)
            {
                case "FreeTest":
                    return new AccountManager(new FreeAccountTestRepository());
                case "BasicTest":
                    return new AccountManager(new BasicAccountTestRepository());
                case "PremiumTest":
                    return new AccountManager(new PremiumAccountTestRepository());
                case "FileTest":
                    return new AccountManager(new FileAccountRepository(@"C:\Users\ejsim\OneDrive\Documents\The Software Guild\Repos\Bitbucket\eric-simpson-individual-work\SGBank\SGBank\SGBank.Data\Accounts.txt"));
                default:
                    throw new Exception("Mode value app config is not valid.");

            }
        }
    }
}
