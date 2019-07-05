using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingEntities;

namespace BankingDataAccessLayer
{
    public class AccountDAL
    {
        //Follows a GET-POST-UPDATE-DELETE design pattern
        //GET operations
        public IAccount GetAccountDAL(User user, int givenAccountID)
        {
            //Parameters:
            // user             - Ideally a user object returned
            //                    from a GetUserDAL() method
            // givenAccountID   - Given Account ID
            var account = user.Accounts.Find(n=>n.AccountId == givenAccountID);
            // Using var we don't have to explicitly state the
            // actual AccountType
            return account;
        }

        //POST operations
        public void CreateAccountDAL(User user,IAccount account)
        {
            //Parameters:
            // user             - Ideally a user object returned
            //                    from a GetUserDAL() method
            // account          - Account we want to create
            user.Accounts.Add(account);
        }

        // UPDATE Operations: Update // Deposit // Withdraw // Transfer
        public void UpdateAccountDAL(User user, IAccount updatedAccount)
        {
            //Parameters:
            // user             - Ideally a user object returned
            //                    from a GetUserDAL() method
            // updatedAccount   - Account we want to save
            int accountIndex = user.Accounts.FindIndex(
                n => n.AccountId == updatedAccount.AccountId);
            user.Accounts[accountIndex] = updatedAccount;
        }
        public void DepositToAccountDAL(User user, int givenAccountID, double depositAmount)
        {
            //Parameters:
            // user             - Ideally a user object returned
            //                    from a GetUserDAL() method
            // givenAccountID   - Given Account ID
            // depositAmount    - Value of deposit we are making
            var account = GetAccountDAL(user, givenAccountID);
            account.CurrentBalance += depositAmount;
            string Transaction = $"User {user.UserId} made a deposit of {depositAmount} " +
                $"to {account.AccountType} {account.AccountId} resulting in a balance of {account.CurrentBalance} ";
            account.Transactions.Add(Transaction);
            UpdateAccountDAL(user,account);
        }

        public void WithdrawFromAccountDAL(User user, int givenAccountID, double withdrawAmount)
        {
            //Parameters:
            // user             - Ideally a user object we'd get
            //                  using a GetUserDAL() method
            // givenAccountID   - Given user ID
            // withdrawAmount   - Value of withdrawal we are making
            var account = GetAccountDAL(user, givenAccountID);
            account.CurrentBalance -= withdrawAmount;
            string Transaction = $"User {user.UserId} made a withdrawl of {withdrawAmount} " +
                $"to {account.AccountType} {account.AccountId} resulting in a balance of {account.CurrentBalance} ";
            account.Transactions.Add(Transaction);
            UpdateAccountDAL(user, account);
        }

        public void TransferAccountDAL(User user, int firstAccountID,
            int secondAccountID, double transferAmount)
        {
            //Parameters:
            // user             - Ideally a user object we'd get
            //                  using a GetUserDAL() method
            // firstAccountID   - Given ID for account we will withdraw from
            // secondAccountID  - Given ID for account we will deposit to
            // transferAmount   - Value of transfer from firstAccount to secondAccount
            //First Account is withdrawn from
            WithdrawFromAccountDAL(user, firstAccountID, transferAmount);
            //Second Account is deposited to
            DepositToAccountDAL(user, secondAccountID, transferAmount);
        }

        //DELETE OPERATION
        public void DeleteAccountDAL(User user,int givenAccountID)
        {
            //Parameters:
            // user             - Ideally a user object returned
            //                    from a GetUserDAL() method
            // givenAccountID   - Given Account ID
            var account = GetAccountDAL(user, givenAccountID);
            user.Accounts.Remove(account);
        }

    }

}
