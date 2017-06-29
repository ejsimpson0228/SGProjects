using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Responses;

namespace SGBank.BLL.WithdrawRules
{
    public class FreeAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account Account, decimal Amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (Account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: a non-free account hit the Free Withdraw Rule. Contact IT!";
                return response;
            }

            if (Amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }

            if (Amount < -100)
            {
                response.Success = false;
                response.Message = "Free accounts cannot withdraw more than $100.";
                return response; 
            }

            if (Account.Balance + Amount < 0)
            {
                response.Success = false;
                response.Message = "Free accounts cannot overdraft!";
                return response;
            }

            response.Success = true;
            response.Account = Account;
            response.Amount = Amount;
            response.OldBalance = Account.Balance;
            Account.Balance += Amount;
            return response;
        }

    }
}
