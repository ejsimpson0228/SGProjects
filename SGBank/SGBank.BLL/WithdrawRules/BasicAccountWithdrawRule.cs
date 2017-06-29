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
    public class BasicAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account Account, decimal Amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (Account.Type != AccountType.Basic)
            {
                response.Success = false;
                response.Message = "Error: a non-basic account hit the Basic Withdraw Rule. Contact IT.";
                return response;
            }

            if (Amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }

            if (Amount < -500)
            {
                response.Success = false;
                response.Message = "Basic accounts cannot withdraw more than $500";
                return response;
            }

            if (Account.Balance + Amount < -100)
            {
                response.Success = false;
                response.Message = "This amount will overdraft more than your $100 limit!";
                return response;
            }

            response.Success = true;
            response.Account = Account;
            response.Amount = Amount;
            response.OldBalance = Account.Balance;
            Account.Balance += Amount;
            if (Account.Balance < 0)
            {
                Account.Balance -= 10;
            }
            return response;
        }
    }
}
