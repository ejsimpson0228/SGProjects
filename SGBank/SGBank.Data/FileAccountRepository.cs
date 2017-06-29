using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private string _filePath;

        public FileAccountRepository (string filePath)
        {
            _filePath = filePath;
        }

        List<Account> accounts = new List<Account>();

        private List<Account> LoadAccounts()

        {
            
            String[] rows = File.ReadAllLines(_filePath);

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Account a = new Account();
                a.AccountNumber = columns[0];
                a.Name = columns[1];
                a.Balance = decimal.Parse(columns[2]);
                if (columns[3] == "F")
                    a.Type = AccountType.Free;
                if (columns[3] == "B")
                    a.Type = AccountType.Basic;
                if (columns[3] == "P")
                    a.Type = AccountType.Premium;

                accounts.Add(a);
                
            }
            
            return accounts;
        }

        private static Account _account = new Account();
        

        public Account LoadAccount(string AccountNumber)
        {
            List<Account> accounts = LoadAccounts();
            foreach (var account in accounts)
            {
                _account = account;

                if (AccountNumber == _account.AccountNumber)
                {
                    return _account; 
                }
                else
                {
                    continue;
                }
            }
            return null;

        }

        public void SaveAccount(Account account)
        {

            _account = account;
            File.Delete(_filePath);
            
            using (StreamWriter sr = new StreamWriter(_filePath))
            {
                sr.WriteLine("AccountNumber,Name,Balance,Type");
                foreach (var a in accounts)
                {
                    string type = "";
                    if (a.Type == AccountType.Basic)
                        type = "B";
                    if (a.Type == AccountType.Free)
                        type = "F";
                    if (a.Type == AccountType.Premium)
                        type = "P";
                    sr.WriteLine($"{a.AccountNumber},{a.Name},{a.Balance},{type}");
                }
            }
            


        }
    }
}
