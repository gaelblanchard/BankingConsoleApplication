using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Our own projects
using BankingEntities;
using BankingDataAccessLayer;


namespace BankingBusinessLayer //Business Layer Function Logic
{
    public static class GetInterestRates {
        public static double BusinessInterest()
        {
            return InterestRates.BusinessInterestRate;
        }
        public static double CheckingInterest()
        {
            return InterestRates.CheckingInterestRate;
        }
        public static double LoanInterest()
        {
            return InterestRates.LoanAccountRate;
        }
        public static double TermDepositInterest()
        {
            return InterestRates.TermDepositRate;
        }

    }
    public class UserBL
    {
        //RegisterMenu
        public int GenerateRandomId()
        {
            int randomId = RandomNumbers.rand.Next();
            return randomId;
        }
        #region
        public void RegisterMenu()
        {
            Console.WriteLine("Do you want to register?");
            Console.WriteLine("Y = yes, N = no");
            string input = Console.ReadLine();
            switch (input)
            {
                case "Y":
                    Console.WriteLine("Enter Registration");
                    //RegisterUser
                    User usertoRegister = new User(GenerateRandomId());
                    RegisterUser(usertoRegister);
                    break;
                case "N":
                    Console.WriteLine("Are You Sure?");
                    Console.WriteLine("Y = yes, N = no");
                    string final_input = Console.ReadLine();
                    switch (final_input)
                    {
                        case "Y":
                            Console.WriteLine("Goodbye.");
                            break;
                        case "N":
                            RegisterMenu();
                            break;
                        default:
                            Console.WriteLine("You will now exit the program");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Not a valid input");
                    break;
            }

        }
        #endregion
        //RegisterUser
        public void RegisterUser(User user)
        {
            user.GoodStanding = true;
            // Add Checks Here
            Console.WriteLine($"Enter First Name for User {user.UserId}: ");
            string userFirstName = Console.ReadLine();
            Console.WriteLine($"Enter Last Name for User {user.UserId}: ");
            string userLastName = Console.ReadLine();
            Console.WriteLine($"Enter SSN for User {user.UserId}: ");
            string userSSN = Console.ReadLine();
            int castedUserSSN = int.Parse(userSSN);
            user.FName = userFirstName;
            user.LName = userLastName;
            user.SSN = castedUserSSN;
            Console.WriteLine($"You have now registered!!");
            UserDAL userData = new UserDAL();
            userData.RegisterUserDAL(user);
            BankingMenu(user);
        }
        public (bool, double) ValidBalance()
        {
            bool isValid = false;
            Console.WriteLine("How much do you want to start the account with?");
            //Add Check
            string desiredBalance = Console.ReadLine();
            double convertedBalance = double.Parse(desiredBalance);
            if (convertedBalance > 0)
            {
                isValid = true;
            }
            return (isValid, convertedBalance);
        }
        public CheckingAccount AddChecking(User user, double givenBalance)
        {
            //AddCheckingDAL
            CheckingAccount newChkAccount = new CheckingAccount(givenBalance);
            UserDAL userDAL = new UserDAL();
            return newChkAccount;
        }
        public BusinessAccount AddBusiness(User user, double givenBalance)
        {
            //AddBusinessDAL
            BusinessAccount newBizAccount = new BusinessAccount(givenBalance);
            UserDAL userDAL = new UserDAL();
            return newBizAccount;

        }
        public LoanAccount AddLoan(User user,double givenBalance)
        {
            //AddLoanDAL
            LoanAccount newLoanAccount = new LoanAccount(givenBalance);
            UserDAL userDAL = new UserDAL();
            return newLoanAccount;

        }
        //Withdraw methods
        public bool Withdraw(User user,CheckingAccount account, Principal principal)
        {
            double resultingBalance = account.CurrentBalance - principal.AmountInvolved;
            bool canWithdraw;
            if (resultingBalance < 0)
            {
                canWithdraw = false;
            }
            else
            {
                int WithdrawIndex = user.Accounts.FindIndex(n => n.AccountId == account.AccountId);
                account.CurrentBalance -= principal.AmountInvolved;
                string actionString = $"User {user.UserId} withdrew {principal.AmountInvolved} from Account {account.AccountId}";
                account.Transactions.Add(actionString);
                user.Accounts[WithdrawIndex] = account;
                canWithdraw = true;
            }
            return canWithdraw;
        }
        public bool Withdraw(User user, BusinessAccount account, Principal principal)
        {
            double resultingBalance = account.CurrentBalance - principal.AmountInvolved;
            bool canWithdraw;
            if (resultingBalance < 0)
            {
                if (account.CurrentBalance < 0)
                {
                    canWithdraw = false;
                }
                else
                {
                    int WithdrawIndex = user.Accounts.FindIndex(n => n.AccountId == account.AccountId);
                    account.CurrentBalance -= principal.AmountInvolved;
                    account.CanClose = false;
                    account.IsOverdraft = true;
                    account.OverDraftAmount = -(account.CurrentBalance);
                    user.GoodStanding = false;
                    user.Accounts[WithdrawIndex] = account;
                    string actionString = $"User {user.UserId} withdrew {principal.AmountInvolved} from Account {account.AccountId} resulting in an overdraft of {account.OverDraftAmount}";
                    account.Transactions.Add(actionString);
                    canWithdraw = true;
                }
            }
            else
            {
                if (account.IsOverdraft)
                {
                    canWithdraw = false;
                }
                else
                {
                    int WithdrawIndex = user.Accounts.FindIndex(n => n.AccountId == account.AccountId);
                    account.CurrentBalance -= principal.AmountInvolved;
                    string actionString = $"User {user.UserId} withdrew {principal.AmountInvolved} from Account {account.AccountId}";
                    account.Transactions.Add(actionString);
                    user.Accounts[WithdrawIndex] = account;
                    canWithdraw = true;
                }
            }
            return canWithdraw;
        }
        public bool Withdraw(User user,LoanAccount account, Principal principal)
        {
            bool canWithdraw;
            double withdrawal = principal.AmountInvolved;
            double resultingBalance = account.CurrentBalance - principal.AmountInvolved;

            // First we check maxBalance threshold
            if (resultingBalance < account.MaximumBalance)
            {
                canWithdraw = false;
            }
            else
            {
                // withdrawl can't be more than maximum withdrawal
                if (withdrawal <= account.MaximumWithdrawal)
                {
                    int WithdrawIndex = user.Accounts.FindIndex(n => n.AccountId == account.AccountId);
                    account.CurrentBalance -= withdrawal;
                    string actionString = $"User {user.UserId} withdrew {withdrawal} from Account {account.AccountId}";
                    account.Transactions.Add(actionString);
                    user.Accounts[WithdrawIndex] = account;
                    canWithdraw = true;
                }
                else
                {
                    canWithdraw = false;
                }
            }

            return canWithdraw;
        }
        // Deposit
        public bool Deposit(User user,IAccount account, Principal principal)
        {
            bool canDeposit = false;
            if (principal.AmountInvolved > 0)
            {
                //
                int DepIndex = user.Accounts.FindIndex(n => n.AccountId == account.AccountId);
                account.CurrentBalance += principal.AmountInvolved;
                string actionString = $"User {user.UserId} deposited {principal.AmountInvolved} to Account {account.AccountId}";
                account.Transactions.Add(actionString);
                user.Accounts[DepIndex] = account;
                canDeposit = true;
            }
            else
            {
                canDeposit = false;
            }
            return canDeposit;
        }
        //Term Deposit
        public bool TermDeposit(IAccount account, Principal principal)
        {
            bool canDeposit = false;
            if (principal.AmountInvolved > 0)
            {
                canDeposit = true;
            }
            else
            {
                canDeposit = false;
            }
            return canDeposit;
        }
        // Checks current standing of user
        public bool StandingCheck(User user)
        {
            bool GoodStanding = false;
            return GoodStanding;
        }
        // Unapplied principal
        public void UnappliedPrincipalCheck(User user)
        {
            //checks unapplied principal
            // and applies it to account with interest
            foreach(IAccount acc in user.Accounts)
            {
                foreach(Principal principal in acc.UnappliedPrincipal)
                {
                    //checks datetime
                    // if interest is to be applied adjusts start date
                    // and date of matriculation
                    // If date of matriculation has passed remove object
                    // and apply to account
                }
            }
        }

        public void BankingMenu(User user)
        {
            bool Proceed = true;
            do
            {
                // The actual menu
                bool AtLeastOneAccount = (user.Accounts.Count >= 1);
                bool AtLeastTwoAccounts = (user.Accounts.Count >= 2);
                Console.WriteLine("What do you want to do");
                Console.WriteLine("O = Open new account");
                if (AtLeastOneAccount)
                {
                    Console.WriteLine("C = Close account");
                    Console.WriteLine("W = Make a withdrawal");
                    Console.WriteLine("D = Make a deposit");
                    Console.WriteLine("L = Display a List of Accounts");
                    Console.WriteLine("T = Display a List of Transactions");
                }
                if (AtLeastTwoAccounts)
                {
                    Console.WriteLine("Z = Make a transfer");
                }
                Console.WriteLine("E = exit");

                string menuInput = Console.ReadLine();
                switch(menuInput){
                    case "O":
                        //Open Account
                        if (user.GoodStanding)
                        {
                            Console.WriteLine("What type of account do you want to make?:");
                            Console.WriteLine("C = checking, B = business, L = loan");
                            string openAccountInput = Console.ReadLine();
                            //bool isValid;
                            //double balance;
                            switch (openAccountInput)
                            {
                                case "C":
                                    Console.WriteLine("Checking Account");
                                    (bool isValid,double Vbalance) =ValidBalance();
                                    if (isValid){
                                        CheckingAccount chk = AddChecking(user, Vbalance);
                                        user.Accounts.Add(chk);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Balance given. Returning to main menu");
                                    }
                                    break;
                                case "B":
                                    Console.WriteLine("Business Account");
                                    (isValid, Vbalance) = ValidBalance();
                                    if (isValid)
                                    {
                                        BusinessAccount biz = AddBusiness(user,Vbalance);
                                        user.Accounts.Add(biz);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Balance given. Returning to main menu");
                                    }
                                    break;
                                case "L":
                                    Console.WriteLine("Loan Account");
                                    (isValid, Vbalance) = ValidBalance();
                                    if (isValid)
                                    {
                                        LoanAccount loan = AddLoan(user, Vbalance);
                                        user.Accounts.Add(loan);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Balance given. Returning to main menu");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Invalid Input");
                                    break;
                            }

                            //Create a principal object
                            // If negative exit
                            // else build account
                        }
                        else
                        {
                            Console.WriteLine("One or more of your accounts are not in good standing");
                            Console.WriteLine("Therefore you can not create a new account");
                        }
                        break;
                    case "C":
                        if (AtLeastOneAccount)
                        {
                            Console.WriteLine($"User {user.UserId}: {user.FName} {user.LName}");
                            foreach (IAccount acc in user.Accounts)
                            {
                                Console.WriteLine($"Account ID: {acc.AccountId} Current Balance: {acc.CurrentBalance}");
                            }

                            Console.WriteLine("Which account do you want to close");
                            Console.WriteLine("Enter the Account ID: ");
                            string givenId = Console.ReadLine();
                            // Give check for parse operation
                            int convertedId = int.Parse(givenId);
                            // search for account by ID
                            int IdMatch = user.Accounts.FindIndex(n => n.AccountId == convertedId);
                            if (IdMatch < 0)
                            {
                                Console.WriteLine("Account Id Input Invalid");
                            }
                            else
                            {
                                if (user.Accounts[IdMatch].CanClose)
                                {
                                    user.Accounts.RemoveAt(IdMatch);
                                }
                                else
                                {
                                    Console.WriteLine("Account could not be closed!");
                                }
                            }
                 
                        }
                        else
                        {
                            Console.WriteLine("There are no accounts available to close!");
                        }
                        break;
                    case "W":
                        if (AtLeastOneAccount)
                        {
                            Console.WriteLine("Withdraw Selected");
                            Console.WriteLine("Select which account ID they want to withdraw from:");

                            string givenId = Console.ReadLine();
                            // Give check for parse operation
                            int convertedId = int.Parse(givenId);
                            // search for account by ID
                            int IdMatch = user.Accounts.FindIndex(n => n.AccountId == convertedId);
                            if (IdMatch < 0)
                            {
                                Console.WriteLine("Account Id Input Invalid");
                            }
                            else
                            {
                                Console.WriteLine("Enter Amount you want to withdraw:");
                                string Value = Console.ReadLine();
                                // Give check for parse operation
                                double convertedValue = int.Parse(Value);
                                // Check if negative run check
                                Principal principal = new Principal
                                {
                                    AmountInvolved = convertedValue
                                };
                                if (user.Accounts[IdMatch] is CheckingAccount)
                                {
                                    bool result = Withdraw(user,(CheckingAccount)user.Accounts[IdMatch], principal);
                                    if (result){
                                        Console.WriteLine("Withrawal completed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrawal invalid!");
                                    }
                                }else if (user.Accounts[IdMatch] is BusinessAccount)
                                {
                                    bool result = Withdraw(user,(BusinessAccount)user.Accounts[IdMatch], principal);
                                    if (result){
                                        Console.WriteLine("Withrawal completed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrawal invalid!");
                                    }
                                }
                                else if (user.Accounts[IdMatch] is LoanAccount)
                                {
                                    bool result = Withdraw(user,(LoanAccount)user.Accounts[IdMatch], principal);
                                    if (result)
                                    {
                                        Console.WriteLine("Withrawal completed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrawal invalid!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Type not detected!!");
                                }
                            }


                            //Creates Principal Object
                            // If amount is negative
                            // Sends message to user and exits option
                            //Select account to withdraw value from
                            // Checks type of account
                            // Checking if resulting balance falls below zero then
                            // exits program
                            // Checking can not fall under 0 as a result of a withdrawal
                            // Business can not withdraw after overdraft
                            // Loan can be withdrew from but only up to 10%
                            // of original loan cost
                            // And can not go over/under maximum balance which is 30%
                            // over starting balance
                        }
                        else
                        {
                            Console.WriteLine("There are no accounts to withdraw from!");
                        }
                        break;
                    case "D":
                        if (AtLeastOneAccount)
                        {
                            Console.WriteLine("Select which account ID you want to withdraw from:");

                            string givenId = Console.ReadLine();
                            // Give check for parse operation
                            int convertedId = int.Parse(givenId);
                            // search for account by ID
                            int IdMatch = user.Accounts.FindIndex(n => n.AccountId == convertedId);

                            if (IdMatch < 0)
                            {
                                Console.WriteLine("Account ID is invalid");
                            }
                            else
                            {
                                Console.WriteLine("Select an option");
                                Console.WriteLine("T = make a term deposit");
                                Console.WriteLine("D = make a regulae deposit");


                                string depositInput = Console.ReadLine();
                                switch (depositInput)
                                {
                                    case "T":
                                        Console.WriteLine("Make a term deposit");
                                        // Create a principal object
                                        //Can not be negative
                                        // Exits option
                                        // Set start date and matriculation date
                                        Console.WriteLine("Enter Amount you want to deposit:");
                                        string Value = Console.ReadLine();
                                        // Give check for parse operation
                                        double convertedValue = int.Parse(Value);
                                        // Check if negative run check
                                        Principal principal = new Principal
                                        {
                                            AmountInvolved = convertedValue
                                        };
                                        //Add checks
                                        Console.WriteLine("Enter Month of Maturity");
                                        string month = Console.ReadLine();
                                        int convertedMonth = int.Parse(month);
                                        Console.WriteLine("Enter Day of Maturity");
                                        string day = Console.ReadLine();
                                        int convertedDay = int.Parse(day);
                                        Console.WriteLine("Enter Year of Maturity");
                                        string year = Console.ReadLine();
                                        int convertedYear = int.Parse(year);

                                        DateTime dateMatriculation = new DateTime(convertedYear,convertedMonth,convertedDay);
                                        Principal termDepositPrincipal = new Principal(DateTime.Now,dateMatriculation, 30)
                                        {
                                            AmountInvolved = convertedValue
                                        };
                                        
                                        break;
                                    case "D":
                                        Console.WriteLine("Make a Deposit");

                                        Console.WriteLine("Enter Amount you want to deposit:");
                                        string depValue = Console.ReadLine();
                                        // Give check for parse operation
                                        double convertedDepValue = int.Parse(depValue);
                                        // Check if negative run check
                                        Principal depPrincipal = new Principal
                                        {
                                            AmountInvolved = convertedDepValue
                                        };
                                        bool deposited = Deposit(user,user.Accounts[IdMatch], depPrincipal);
                                        if (deposited)
                                        {
                                            Console.WriteLine("Deposit completed");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Deposit not completed");
                                        }
                                        // If statements to run loan check and business check
                                        //Can not be negative
                                        // Then it should work
                                        break;
                                    default:
                                        Console.WriteLine("You selected an invalid option");
                                        break;
                                }
                                // Case statement T, D and Default

                            }

                        }
                        else
                        {
                            Console.WriteLine("There are no accounts to deposit to!");
                        }
                        break;
                    case "L":
                        if (AtLeastOneAccount)
                        {
                            Console.WriteLine($"User {user.UserId}: {user.FName} {user.LName}");
                            foreach (IAccount acc in user.Accounts)
                            {
                                Console.WriteLine($"Account {acc.AccountId} Current Balance: {acc.CurrentBalance}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no accounts available for diplay");
                        }
   
                        break;
                    case "T":
                        if (AtLeastOneAccount)
                        {
                            foreach(IAccount acc in user.Accounts){
                                if (acc.Transactions.Count > 0) {
                                    foreach (string transaction in acc.Transactions)
                                    {
                                        Console.WriteLine(transaction);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Account {acc.AccountId} has no transactions");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No transactions to report");
                        }
                        break;
                    case "Z":
                        if (AtLeastTwoAccounts)
                        {
                            Console.WriteLine("Select which account ID you want to withdraw from:");

                            string withdrawId = Console.ReadLine();
                            // Give check for parse operation
                            int convertedWithdrawId = int.Parse(withdrawId);
                            // search for account by ID
                            int WithdrawIdMatch = user.Accounts.FindIndex(n => n.AccountId == convertedWithdrawId);
                            if(WithdrawIdMatch < 0)
                            {
                                Console.WriteLine("Account Does not exist. Therefore we can not complete this transfer.");
                            }
                            else
                            {
                                Console.WriteLine("Enter Amount you want to withdraw:");
                                string Value = Console.ReadLine();
                                // Give check for parse operation
                                double convertedValue = int.Parse(Value);
                                // Check if negative run check
                                Principal principal = new Principal
                                {
                                    AmountInvolved = convertedValue
                                };
                                bool withdrawResult = true;
                                if (user.Accounts[WithdrawIdMatch] is CheckingAccount)
                                {
                                    bool result = Withdraw(user, (CheckingAccount)user.Accounts[WithdrawIdMatch], principal);
                                    if (result)
                                    {
                                        Console.WriteLine("Withrawal completed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrawal invalid!");
                                    }
                                    withdrawResult = result;
                                }
                                else if (user.Accounts[WithdrawIdMatch] is BusinessAccount)
                                {
                                    bool result = Withdraw(user, (BusinessAccount)user.Accounts[WithdrawIdMatch], principal);
                                    if (result)
                                    {
                                        Console.WriteLine("Withrawal completed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrawal invalid!");
                                    }
                                    withdrawResult = result;
                                }
                                else if (user.Accounts[WithdrawIdMatch] is LoanAccount)
                                {
                                    bool result = Withdraw(user, (LoanAccount)user.Accounts[WithdrawIdMatch], principal);
                                    if (result)
                                    {
                                        Console.WriteLine("Withrawal completed.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Withdrawal invalid!");
                                    }
                                    withdrawResult = result;
                                }
                                else
                                {
                                    Console.WriteLine("Type not detected!!");
                                    withdrawResult = false;
                                }
                                if (withdrawResult)
                                {
                                    Console.WriteLine("Enter Account Id you want to deposit to");
                                    string depositId = Console.ReadLine();
                                    // Give check for parse operation
                                    int convertedDepositId = int.Parse(depositId);
                                    // search for account by ID
                                    int DepositIdMatch = user.Accounts.FindIndex(n => n.AccountId == convertedDepositId);
                                    if (DepositIdMatch < 0) {
                                        Console.WriteLine("Id is invalid. Depositing funds back into original account!");
                                        bool deposited = Deposit(user, user.Accounts[WithdrawIdMatch], principal);

                                    }
                                    else
                                    {
                                        bool deposited = Deposit(user, user.Accounts[DepositIdMatch], principal);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Withdraw Could not be completed! Transfer aborted!");
                                }

                            }

                            // Creates principal object
                            // if negative exits
                            // Get first account
                            //Tries first withdraw if that does not work
                            //break
                            //Gets second account
                            //Tries deposit which will always work when valid
                            // Perform Transfer
                            // Transfer has it's own catches
                        }
                        else
                        {
                            Console.WriteLine("A transfer is not possible without two accounts");
                        }
                        break;
                    case "E":
                        Console.WriteLine("Exit Option Selected");
                        Proceed = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }

            } while (Proceed);
            Console.WriteLine("You will now exit the Banking System");
            
        }
    }


}
