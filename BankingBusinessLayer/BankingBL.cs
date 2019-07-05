using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Other projects in solutions
using BankingEntities;
using BankingDataAccessLayer;


namespace BankingBusinessLayer //Business Layer Function Logic
{
    public partial class UserBL
    {
        //User BL implements all the business logic
        public User Login(string UserName, string Password)
        {
            // Accesses Data Layer to LogInUser
            UserDAL userDAL = new UserDAL();
            return userDAL.GetUserDAL(UserName,Password);
        }
        public void AtLeastOneAccountOptions()
        {
            Console.WriteLine("C = Close account");
            Console.WriteLine("W = Make a withdrawal");
            Console.WriteLine("D = Make a deposit");
            Console.WriteLine("L = Display a List of Accounts");
            Console.WriteLine("T = Display a List of Transactions by Account");

        }
        public void AtLeastTwoAccountsOptions()
        {
            Console.WriteLine("Z = Make a transfer");
        }
        public void OpenAccountDialogue()
        {
            Console.WriteLine("What type of account do you want to make?:");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("C = checking, B = business, L = loan, T = term deposit");
        }
        public void BadStandingDialogue()
        {
            Console.WriteLine("One or more of your accounts are not in good standing");
            Console.WriteLine("Therefore you can not create a new account");
        }
        public void WithdrawDialogue()
        {
            Console.WriteLine("Withdraw Selected");
            Console.WriteLine("Select which account ID you want to withdraw from:");
        }
        public void DepositDialogue()
        {
            Console.WriteLine("Deposit Selected");
            Console.WriteLine("Select which account ID you want to deposit to:");
        }
    }


}
