using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Other projects in solutions
using BankingEntities;
using BankingDataAccessLayer;

namespace BankingBusinessLayer
{
    public partial class UserBL
    {
        //StandingCheckAndSet
        public void StandingCheckAndSet(User user)
        {
            bool goodStanding = true;
            foreach(IAccount acc in user.Accounts)
            {
                if (acc is BusinessAccount)
                {
                    BusinessAccount biz = (BusinessAccount)acc;
                    if(biz.CurrentBalance < 0)
                    {
                        ((BusinessAccount)acc).IsOverdraft = true;
                        ((BusinessAccount)acc).OverDraftAmount = ((BusinessAccount)acc).CurrentBalance ;
                        goodStanding = false;
                    }
                }
                if (acc is LoanAccount )
                {
                    LoanAccount loan = (LoanAccount)acc;
                    if (Math.Abs(loan.CurrentBalance - loan.MaximumBalance) < double.Epsilon)
                    {
                        goodStanding = false;
                    }
                }
            }
            user.GoodStanding = goodStanding;
            UserDAL userDAL = new UserDAL();
            userDAL.UpdateUserDAL(user);
        }
        //Display all Accounts
        public void DisplayAllAccounts(User user)
        {
            Console.WriteLine($"User {user.UserId}: {user.FName} {user.LName}");
            foreach (IAccount acc in user.Accounts)
            {
                Console.WriteLine($"{acc.AccountType} Account ID: {acc.AccountId} Current Balance: {acc.CurrentBalance}");
            }
        }
        // Delete Check
        public bool CanDelete(User user, int givenAccountID)
        {
            bool deletePossible = false;
            if (user.GoodStanding)
            {
                AccountDAL accountDAL = new AccountDAL();
                var account = accountDAL.GetAccountDAL(user, givenAccountID);
                if(account.CurrentBalance > 0)
                {
                    deletePossible = true;
                    return deletePossible;
                }
                else
                {
                    return deletePossible;
                }
            }
            else
            {
                return deletePossible;
            }
        }
        //Delete Operation
        public void DeleteAccountBL(User user, int givenAccountID)
        {
            bool canDelete = CanDelete(user, givenAccountID);
            if (canDelete)
            {
                AccountDAL accountDAL = new AccountDAL();
                accountDAL.DeleteAccountDAL(user, givenAccountID);

            }
            else
            {
                Console.WriteLine("Account can not be deleted!");
            }
        }
        // UPDATE
        // Withdraw Check
        public bool CanWithdraw(User user, int givenAccountID, double withdrawAmount)
        {
            bool withdrawPossible;
            AccountDAL accountDAL = new AccountDAL();
            var account = accountDAL.GetAccountDAL(user, givenAccountID);
            double remainingBalance = account.CurrentBalance - withdrawAmount;
            switch (account.AccountType)
            {
                case "Checking Account":
                    if(remainingBalance > 0)
                    {
                        withdrawPossible = true;
                    }
                    else
                    {
                        withdrawPossible = false;
                    }
                    break;
                case "Business Account":
                    if (remainingBalance > 0)
                    {
                        withdrawPossible = true;
                    }
                    else
                    {
                        BusinessAccount biz = (BusinessAccount)account;
                        if (biz.IsOverdraft)
                        {
                            withdrawPossible = false;
                        }
                        else
                            withdrawPossible = true;
                    }
                    break;
                case "Loan Account":
                    LoanAccount loan = (LoanAccount)account;
                    if(withdrawAmount > loan.MaximumWithdrawal)
                    {
                        withdrawPossible = false;
                    }
                    else
                    {
                        if(remainingBalance > loan.MaximumBalance)
                        {
                            withdrawPossible = true;
                        }
                        else
                        {
                            withdrawPossible = false;
                        }
                    }
                    break;
                case "Term Deposit":
                    if(remainingBalance > 0)
                    {
                        withdrawPossible = true;
                    }
                    else
                    {
                        withdrawPossible = false;
                    }
                    break;
                default:
                    withdrawPossible = false;
                    Console.WriteLine("Type not detected. Major Failure!");
                    break;

            }
            return withdrawPossible;
        }
        // Withdraw
        public void WithdrawAccountBL(User user, int givenAccountID,double withdrawAmount)
        {
            bool canWithdraw = CanWithdraw(user, givenAccountID, withdrawAmount);
            if (canWithdraw)
            {
                AccountDAL accountDAL = new AccountDAL();
                accountDAL.WithdrawFromAccountDAL(user, givenAccountID, withdrawAmount);

            }
            else
            {
                Console.WriteLine("This withdrawal can not be made!");
            }
        }
        // Deposit
        public void DepositAccountBL(User user, int givenAccountID, double depositAmount)
        {
            // Check is not required bc a deposit is always
            // valid especially after checks i instituted
            AccountDAL accountDAL = new AccountDAL();
            accountDAL.DepositToAccountDAL(user, givenAccountID, depositAmount);
        }
        //Create
        public void CreateAccountBL(User user, IAccount account)
        {
            AccountDAL accountDAL = new AccountDAL();
            accountDAL.CreateAccountDAL(user, account);
        }
        
    }
}