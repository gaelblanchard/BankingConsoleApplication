using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    public interface IAccount
    {
        // Interface for all accounts all of our account types
        // implement this interface
        int AccountId { get; set; }
        string AccountType { get; set; }
        double CurrentBalance { get; set; }
        double InterestRate { get; set; }
        int InterestTerm { get; set; }
        bool CanClose { get; set; }
        List<string> Transactions { get; set; }
    }

    // Account Classes: Checking, Business, Loan, TermDeposit
    public class CheckingAccount : IAccount
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public int InterestTerm { get; set; }
        public bool CanClose { get; set; }
        public List<String> Transactions { get; set; }
        public CheckingAccount(double givenBalance)
        {
            AccountId = RandomNumbers.rand.Next();
            AccountType = "Checking Account";
            CurrentBalance = givenBalance;
            InterestRate = InterestRates.CheckingInterestRate;
            InterestTerm = 3;
            CanClose = true;
            Transactions = new List<string>();
            string firstTransaction = $"{AccountType} {AccountId} created " +
                $"on {DateTime.Now.ToString()} with a starting deposit of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
    public class LoanAccount : IAccount
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public int InterestTerm { get; set; }
        public bool CanClose { get; set; }
        public List<String> Transactions { get; set; }
        // Loan Account specific properties
        // A maximum balance so that User doesn't
        // accruse asubstantially greater balance
        // than the original loan amount.
        // A maximum withdrawal restricts how much
        // a User can take at a time
        public double MaximumWithdrawal { get; set; }
        public double MaximumBalance { get; set; }
        public bool IsPaidOff { get; set; }
        public LoanAccount(double givenBalance)
        {
            AccountId = RandomNumbers.rand.Next();
            AccountType = "Loan Account";
            CurrentBalance = -(givenBalance);
            InterestRate = InterestRates.LoanAccountRate;
            MaximumWithdrawal = givenBalance / 10;
            MaximumBalance = CurrentBalance - (3 * (MaximumWithdrawal));
            InterestTerm = 365;
            IsPaidOff = false;
            CanClose = false;
            Transactions = new List<string>();
            string firstTransaction = $"{AccountType} {AccountId} created " +
                $"on {DateTime.Now.ToString()} with an agreed loan in the amount of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
    public class BusinessAccount : IAccount
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public int InterestTerm { get; set; }
        public bool CanClose { get; set; }
        public List<String> Transactions { get; set; }
        // Business Account Specific properties
        // A Business Acoount is the only
        // account that can be overdrawn
        public double OverDraftAmount { get; set; }
        public bool IsOverdraft { get; set; }
        public BusinessAccount(double givenBalance)
        {
            AccountId = RandomNumbers.rand.Next();
            AccountType = "Business Account";
            CurrentBalance = givenBalance;
            OverDraftAmount = 0;
            InterestRate = InterestRates.BusinessInterestRate;
            IsOverdraft = false;
            InterestTerm = 7;
            CanClose = true;
            Transactions = new List<string>();
            string firstTransaction = $"{AccountType} {AccountId} created " +
                $"on {DateTime.Now.ToString()} with a starting deposit of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
    public class TermDepositAccount : IAccount
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public double CurrentBalance { get; set; }
        public double InterestRate { get; set; }
        public int InterestTerm { get; set; }
        public bool CanClose { get; set; }
        public List<string> Transactions { get; set; }
        // Term Deposit Account Specific Properties
        // Penalty Rate is removed at each withdrawal
        // Lowering the Interest Rate gained on a
        // Term Deposit
        public Dictionary<DateTime, double> RemainingPayments { get; set; }
        public DateTime DateDeposited { get; set; }
        public DateTime DateOfMaturity { get; set; }
        public double PenaltyRate { get; set; }
        public TermDepositAccount(double givenBalance,
            DateTime DepositDate,
            DateTime MaturityDate)
        {
            AccountId = RandomNumbers.rand.Next();
            AccountType = "Term Deposit";
            CurrentBalance = givenBalance;
            DateDeposited = DepositDate;
            DateOfMaturity = MaturityDate;
            PenaltyRate = InterestRates.PenaltyRate;
            InterestRate = InterestRates.LoanAccountRate;
            InterestTerm = 365;
            CanClose = true;
            Transactions = new List<string>();
            string firstTransaction = $"{AccountType} {AccountId} created " +
                $"on {DateTime.Now.ToString()} with a starting deposit of {givenBalance}";
            Transactions.Add(firstTransaction);
        }
    }
}
