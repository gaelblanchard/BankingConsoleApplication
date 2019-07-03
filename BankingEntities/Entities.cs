using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    // Generates Random to create Random Ids
    public static class RandomNumbers
    {
        public static Random rand;
        static RandomNumbers()
        {
            rand = new Random();
        }
    }
    // Our interest rates
    public static class InterestRates
    {
        public static double BusinessInterestRate { get; set; }
        public static double CheckingInterestRate { get; set; }
        public static double LoanAccountRate { get; set; }
        public static double TermDepositRate { get; set; }
        static InterestRates()
        {
            BusinessInterestRate = 0.18;
            CheckingInterestRate = 0.23;
            LoanAccountRate = 0.11;
            TermDepositRate = 0.09;
        }

    }
    //Our user class will have all of our options
    // and a list of accounts
    public class User
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int SSN { get; set; }
        public bool GoodStanding { get; set; }
        public List<IAccount> Accounts { get; set; }
        public User(int id)
        {
            // initialize list
            this.UserId = id;
            Accounts = new List<IAccount>();
        }
        public User(int id, string firstName, string lastName, int SSN)
        {
            this.UserId = id;
            this.FName = firstName;
            this.LName = lastName;
            this.SSN = SSN;
            Accounts = new List<IAccount>();
        }
    }
    // Account Classes: Base(Account) Checking, Business, Loan
    public class CheckingAccount : IAccount
    {
        public int AccountId { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public int InterestTerm { get; set; }
        public bool CanClose { get; set; }
        public List<Principal> UnappliedPrincipal { get; set; }
        public List<String> Transactions { get; set; }
        public CheckingAccount(double givenBalance)
        {
            AccountId = RandomNumbers.rand.Next();
            CurrentBalance = givenBalance;
            InterestRate = InterestRates.CheckingInterestRate;
            InterestTerm = 3;
            CanClose = true;
            UnappliedPrincipal = new List<Principal>();
            Transactions = new List<string>();
            string firstTransaction = $"Checking Account {AccountId} created with a starting deposit of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
    public class LoanAccount : IAccount
    {
        public int AccountId { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public double MaximumWithdrawal { get; set; }
        public double MaximumBalance { get; set; }
        public int InterestTerm { get; set; }
        public bool IsPaidOff { get; set; }
        public bool CanClose { get; set; }
        public List<Principal> UnappliedPrincipal { get; set; }
        public List<String> Transactions { get; set; }
        public LoanAccount(double givenBalance)
        {
            AccountId = RandomNumbers.rand.Next();
            CurrentBalance = -(givenBalance);
            InterestRate = InterestRates.LoanAccountRate;
            MaximumWithdrawal = givenBalance / 10;
            MaximumBalance = CurrentBalance - (3 * (MaximumWithdrawal));
            InterestTerm = 365;
            IsPaidOff = false;
            CanClose = false;
            UnappliedPrincipal = new List<Principal>();
            Transactions = new List<string>();
            string firstTransaction = $"Loan Account {AccountId} created with a balance of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
    public class BusinessAccount : IAccount
    {
        public int AccountId { get; set; }
        public double CurrentBalance { get; set; }
        public double OverDraftAmount { get; set; }
        public double InterestRate { get; set; }
        public bool IsOverdraft { get; set; }
        public int InterestTerm { get; set; }
        public bool CanClose { get; set; }
        public List<Principal> UnappliedPrincipal { get; set; }
        public List<String> Transactions { get; set; }
        public BusinessAccount(double givenBalance)
        {
            AccountId = RandomNumbers.rand.Next();
            CurrentBalance = givenBalance;
            OverDraftAmount = 0;
            InterestRate = InterestRates.BusinessInterestRate;
            IsOverdraft = false;
            InterestTerm = 7;
            CanClose = true;
            UnappliedPrincipal = new List<Principal>();
            Transactions = new List<string>();
            string firstTransaction = $"Business Account {AccountId} created with a starting deposit of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
    // Checking Business and Loan will implement this base class
    // Principal just implemnts Principal
    public class Principal
    {
        public double AmountInvolved { get; set; }
        public double InterestRate { get; set; }
        public int InterestTerm { get; set; }
        public bool IsTermDeposit { get; set; }
        public DateTime DateDeposited { get; set; }
        public DateTime DateOfMaturity { get; set; }
        public Principal()
        {
            // Constructor for non Term Deposit
            this.DateDeposited = DateTime.Now;
            this.DateOfMaturity = DateTime.Now;
            this.IsTermDeposit = false;
            this.InterestTerm = 0;
            this.InterestRate = 0;
        }
        public Principal(DateTime DateDeposited, DateTime DateOfMaturity, int InterestTerm)
        {
            // Constructor for Term Deposit
            this.DateDeposited = DateDeposited;
            this.DateOfMaturity = DateOfMaturity;
            this.IsTermDeposit = true;
            this.InterestTerm = InterestTerm;
            this.InterestRate = InterestRates.TermDepositRate;

        }
    }
}
