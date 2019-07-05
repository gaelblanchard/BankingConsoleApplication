using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    //Our user class will have all of our options
    // and a list of accounts
    public class User
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SSN { get; set; }
        public bool GoodStanding { get; set; }
        public List<IAccount> Accounts { get; set; }
        public User(int id)
        {
            // initialize list
            this.UserId = id;
            this.GoodStanding = true;
            Accounts = new List<IAccount>();
            //BankingSystem.Users.Add(this);
        }
        public User(int id, string firstName, string lastName, int SSN)
        {
            this.UserId = id;
            this.FName = firstName;
            this.LName = lastName;
            this.SSN = SSN;
            this.GoodStanding = true;
            Accounts = new List<IAccount>();
            //BankingSystem.Users.Add(this);
        }
    }
    // Checking Business and Loan will implement this base class
    // Principal just implemnts Principal
    //public class Principal
    //{
    //    public double AmountInvolved { get; set; }
    //    public double InterestRate { get; set; }
    //    public int InterestTerm { get; set; }
    //    public bool IsTermDeposit { get; set; }
    //    public DateTime DateDeposited { get; set; }
    //    public DateTime DateOfMaturity { get; set; }
    //    public Principal()
    //    {
    //        // Constructor for non Term Deposit
    //        this.DateDeposited = DateTime.Now;
    //        this.DateOfMaturity = DateTime.Now;
    //        this.IsTermDeposit = false;
    //        this.InterestTerm = 0;
    //        this.InterestRate = 0;
    //    }
    //    public Principal(DateTime DateDeposited, DateTime DateOfMaturity, int InterestTerm)
    //    {
    //        // Constructor for Term Deposit
    //        this.DateDeposited = DateDeposited;
    //        this.DateOfMaturity = DateOfMaturity;
    //        this.IsTermDeposit = true;
    //        this.InterestTerm = InterestTerm;
    //        this.InterestRate = InterestRates.TermDepositRate;

    //    }
    //}
}
