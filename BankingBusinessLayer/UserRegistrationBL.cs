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
        public void RegisterUser(User user)
        {

            Console.WriteLine($"Enter First Name for User {user.UserId}: ");
            (bool isValid, string userFirstName) = ValidName();
            if (isValid)
            {
                Console.WriteLine($"Enter Last Name for User {user.UserId}: ");
                (bool stillValid, string userLastName) = ValidName();
                if (stillValid)
                {
                    Console.WriteLine($"Enter SSN for User {user.UserId}: ");
                    (bool stillValid2,int userSSN) = ValidNumber();
                    if (stillValid2)
                    {
                        Console.WriteLine($"Enter UserName for User {user.UserId}: ");
                        (bool stillValid3, string userUserName) = ValidLoginInfo();
                        if (stillValid3)
                        {
                            Console.WriteLine($"Enter Password for User {user.UserId}: ");
                            (bool stillValid4, string userPassword) = ValidLoginInfo();
                            if (stillValid4)
                            {
                                user.FName = userFirstName;
                                user.LName = userLastName;
                                user.SSN = userSSN;
                                user.UserName = userUserName;
                                user.Password = userPassword;
                                UserDAL userDAL = new UserDAL();
                                userDAL.RegisterUserDAL(user);
                                // Use for Login
                                if (user is null)
                                {
                                    Console.WriteLine("User Was Not Registered!!!");
                                }
                                else
                                {
                                    Console.WriteLine("You have now registered!!");
                                    BankingMenu(user);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("UserName is invalid!");
                            RegisterUser(user);
                        }
                    }
                    else
                    {
                        Console.WriteLine("SSN is invalid!");
                        RegisterUser(user);
                    }
                }
                else
                {
                    Console.WriteLine("Last Name is invalid!");
                    RegisterUser(user);
                }
            }
            else
            {
                Console.WriteLine("First Name is invalid!");
                RegisterUser(user);
            }
            


            //Console.WriteLine($"Enter First Name for User {user.UserId}: ");
            //string userFirstName = Console.ReadLine();
            //bool containsInt = userFirstName.Any(c => char.IsDigit(c));
            //if (containsInt)
            //{
            //    //
            //}
            //else
            //{
            //    Console.WriteLine($"Enter Last Name for User {user.UserId}: ");
            //    string userLastName = Console.ReadLine();
            //    containsInt = userLastName.Any(c => char.IsDigit(c));
            //    if (containsInt)
            //    {
            //        Console.WriteLine("LastName is invalid");
            //        RegisterUser(user);
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Enter SSN for User {user.UserId}: ");
            //        string userSSN = Console.ReadLine();
            //        bool isIntString = userSSN.All(c => char.IsDigit(c));
            //        if (isIntString)
            //        {
            //            int castedUserSSN = int.Parse(userSSN);
            //            user.FName = userFirstName;
            //            user.LName = userLastName;
            //            user.SSN = castedUserSSN;
            //            Console.WriteLine($"You have now registered!!");
            //            UserDAL userData = new UserDAL();
            //            userData.RegisterUserDAL(user);
            //            BankingMenu(user);
            //        }
            //        else
            //        {
            //            Console.WriteLine("Given SSN is not a number");
            //            RegisterUser(user);
            //        }
            //    }

            //}
        }
    }
}
