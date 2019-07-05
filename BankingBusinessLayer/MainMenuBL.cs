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

        public void BankingMenu(User user)
        {
            bool Proceed = true;
            do
            {
                bool AtLeastOneAccount = (user.Accounts.Count >= 1);
                bool AtLeastTwoAccounts = (user.Accounts.Count >= 2);

                // Menu Options
                Console.WriteLine("What do you want to do");
                Console.WriteLine("O = Open new account");
                if (AtLeastOneAccount)
                    AtLeastOneAccountOptions();
                if (AtLeastTwoAccounts)
                    AtLeastTwoAccountsOptions();
                Console.WriteLine("A = Logout of Account");
                Console.WriteLine("E = Exit Program");

                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "O":
                        if (user.GoodStanding){
                            OpenAccountDialogue();
                            string openAccountInput = Console.ReadLine();
                            switch (openAccountInput)
                            {
                                case "C":
                                    (bool isValid, double Vbalance) = ValidStartingBalance("Checking Account");
                                    if (isValid)
                                    {
                                        CheckingAccount chk = new CheckingAccount(Vbalance);
                                        CreateAccountBL(user, chk);
                                        //user.Accounts.Add(chk);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Balance given. Returning to main menu");
                                    }
                                    break;
                                case "B":
                                    (isValid, Vbalance) = ValidStartingBalance("Business Account");
                                    if (isValid)
                                    {
                                        BusinessAccount biz = new BusinessAccount(Vbalance);
                                        CreateAccountBL(user, biz);
                                        //user.Accounts.Add(biz);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Balance given. Returning to main menu");
                                    }
                                    break;
                                case "L":
                                    (isValid, Vbalance) = ValidStartingBalance("Loan Account");
                                    if (isValid)
                                    {
                                        LoanAccount loan = new LoanAccount(Vbalance);
                                        CreateAccountBL(user, loan);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Balance given. Returning to main menu");
                                    }
                                    break;
                                case "T":
                                    Console.WriteLine("Creating a Term Deposit. Interest is applied every 3 months.");
                                    (isValid, Vbalance) = ValidStartingBalance("Term Deposit");
                                    if (isValid)
                                    {
                                        Console.WriteLine("Enter year of Maturity:");
                                        (bool isYearValid, int maturityYear) = ValidYear();
                                        if (isYearValid)
                                        {
                                            Console.WriteLine("Enter day of Maturity:");
                                            (bool isDayValid, int maturityDay) = ValidDay();
                                            if (isDayValid)
                                            {
                                                Console.WriteLine("Enter month of Maturity:");
                                                (bool isMonthValid, int maturityMonth) = ValidMonth();
                                                if (isMonthValid)
                                                {
                                                    DateTime MaturityDate = new DateTime(maturityYear, maturityMonth, maturityDay);
                                                    TermDepositAccount termDeposit = new TermDepositAccount(Vbalance, DateTime.Now, MaturityDate);
                                                    CreateAccountBL(user,termDeposit);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Month is not valid!");
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine("Day is invalid!");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Year is invalid!");
                                        }
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
                        }
                        else
                        {
                            BadStandingDialogue();
                        }
                        break;
                    case "C":
                        if (AtLeastOneAccount)
                        {
                            DisplayAllAccounts(user);
                            Console.WriteLine("Which account do you want to close");
                            Console.WriteLine("Enter the Account ID: ");
                            (bool validId, int givenId) = ValidNumber();
                            if (validId)
                            {
                                bool idInAccounts = ExistantID(user, givenId);
                                if (idInAccounts)
                                {
                                    DeleteAccountBL(user, givenId);
                                }
                                else
                                {
                                    Console.WriteLine($"User does not have an account {givenId}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No accounts to close!");
                        }
                        break;
                    case "W":
                        if (AtLeastOneAccount)
                        {
                            WithdrawDialogue();
                            (bool isValidId, int givenId) = ValidNumber();
                            if (isValidId)
                            {
                                bool idInAccounts = ExistantID(user, givenId);
                                if (idInAccounts)
                                {
                                    (bool isAmountValid, double withdrawAmount) = ValidPrincipal("Withdrawal");
                                    if (isAmountValid)
                                    {
                                        WithdrawAccountBL(user, givenId, withdrawAmount);
                                        StandingCheckAndSet(user);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{withdrawAmount} is not valid for withdrawal!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"User {user.UserId} has no Account {givenId}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Id is not valid!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no accounts to withdraw from!");
                        }
                        break;
                    case "D":
                        if (AtLeastOneAccount)
                        {
                            DepositDialogue();
                            (bool isIdValid,int givenId) = ValidNumber();
                            if (isIdValid)
                            {
                                bool idInAccounts = ExistantID(user, givenId);
                                if (idInAccounts)
                                {
                                    (bool isAmountValid, double depositAmount) = ValidPrincipal("Withdrawal");
                                    if (isAmountValid)
                                    {
                                        DepositAccountBL(user, givenId, depositAmount);
                                        StandingCheckAndSet(user);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{depositAmount} is not valid for withdrawal!");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine($"User {user.UserId} has no Account {givenId}");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Id is not valid!");
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
                            DisplayAllAccounts(user);
                        }
                        else
                        {
                            Console.WriteLine("No accounts to display!");
                        }
                        break;
                    case "T":
                        if (AtLeastOneAccount)
                        {

                        }
                        else
                        {
                            Console.WriteLine("There are no accounts to list transactions for!");
                        }
                        break;
                    case "Z":
                        if (AtLeastTwoAccounts)
                        {

                        }
                        else
                        {
                            Console.WriteLine("There are not enough accounts to perform a transaction!");
                        }
                        break;
                    case "A":
                        Console.WriteLine("LogOut Option Selected");
                        break;
                    case "E":
                        Proceed = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }

                // Menu Flow


            } while (Proceed);
        }
        
    }
}