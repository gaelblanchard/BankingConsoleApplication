using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    public static class RandomNumbers
    {
        //Generates Random Integer to create unique Random Ids
        //Note: rand.Next() returns a unique id everytime
        public static Random rand;
        static RandomNumbers()
        {
            rand = new Random();
        }
        public static int GenerateRandomId()
        {
            return RandomNumbers.rand.Next();
        }
    }

    public static class InterestRates
    {
        // Interest rates for various accounts
        public static double BusinessInterestRate { get; set; }
        public static double CheckingInterestRate { get; set; }
        public static double LoanAccountRate { get; set; }
        public static double TermDepositRate { get; set; }
        public static double PenaltyRate { get; set; }
        static InterestRates()
        {
            BusinessInterestRate = 0.18;
            CheckingInterestRate = 0.23;
            LoanAccountRate = 0.11;
            TermDepositRate = 0.09;
            PenaltyRate = 0.02;
        }

    }
}