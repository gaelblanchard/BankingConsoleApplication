using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    // Interface we will use to form all our classes
    // Interest Term && Term are recorded in days
    public interface IAccount
    {
        int AccountId { get; set; }
        double CurrentBalance { get; set; }
        double InterestRate { get; set; }
        int InterestTerm { get; set; }
        bool CanClose { get; set; }
        List<Principal> UnappliedPrincipal { get; set; }
        List<string> Transactions { get; set; }
        //bool Withdraw(Principal principal);
        //bool Deposit(Principal principal);
        //bool TermDeposit(Principal principal);
        //void TermDepositCheck(Principal principal);
    }
}
