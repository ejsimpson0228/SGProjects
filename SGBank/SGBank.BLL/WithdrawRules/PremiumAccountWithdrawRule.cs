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
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account Account, decimal Amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (Account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: a non-basic account hit the Premium Withdraw Rule. Contact IT.";
                return response;
            }

            if (Amount >= 0)
            {
                response.Success = false;
                response.Message = "Withdrawal amounts must be negative!";
                return response;
            }

            if (Amount + Account.Balance < -500)
            {
                response.Success = false;
                response.Message = "This amount will overdraft more than your $500 limit";
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
