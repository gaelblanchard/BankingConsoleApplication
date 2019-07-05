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
        public void StartUpMenu()
        {
            // StartUpMenu gives user the option to
            // register or exit program
            Console.WriteLine("Do you want to register?");
            Console.WriteLine("Y = yes, N = no");
            string input = Console.ReadLine();
            switch (input)
            {
                case "Y":
                    
                    Console.WriteLine("Entering Registration");
                    User userToRegister = new User(RandomNumbers.GenerateRandomId());
                    RegisterUser(userToRegister);
                    break;

                case "N":

                    Console.WriteLine("Are You Sure?");
                    Console.WriteLine("Y = yes, N = no");
                    string finalInput = Console.ReadLine();
                    switch (finalInput)
                    {
                        case "Y":

                            Console.WriteLine("You will now exit the program.");
                            break;

                        case "N":

                            StartUpMenu();
                            break;

                        default:

                            Console.WriteLine("You will now exit the program.");
                            break;
                    }
                    break;

                default:

                    Console.WriteLine("Not a valid input");
                    Console.WriteLine("Do you want to continue?");
                    Console.WriteLine("Y = yes, N = no");
                    string sureInput = Console.ReadLine();

                    switch (sureInput)
                    {
                        case "Y":

                            Console.WriteLine("You will now exit the program");
                            break;

                        case "N":

                            StartUpMenu();
                            break;

                        default:

                            Console.WriteLine("You will now exit the program");
                            break;

                    }
                    break;

            }

        }

    }

}