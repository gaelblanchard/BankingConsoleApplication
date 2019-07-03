using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Our own code
using BankingBusinessLayer;
using BankingEntities;


namespace Banking //Front End
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to the Banking App!");
            Console.WriteLine("This bank allows you to register as a User");
            Console.WriteLine("and open accounts and take loans where you can withdraw, deposit");
            Console.WriteLine("and transfer funds.");
            // Declare User object
            UserBL userBanking = new UserBL();
            userBanking.RegisterMenu();
            //Console.WriteLine(user.UserId);

            // Registration option

            //Main Menu

            //Add Account
        }
    }
}
