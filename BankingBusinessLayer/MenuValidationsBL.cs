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
        public bool ExistantID(User user,int givenId)
        {
            // Determines if id is existant
            // in User's list of accounts
            int IdMatch = user.Accounts.FindIndex(n => n.AccountId == givenId);
            if (IdMatch < 0)
            {
                return false;
            }
            else
                return true;
        }
        public (bool,double) ValidateAmount()
        {
            // Validates that amount given
            // is positive
            bool isValid = false;
            double convertedBalance;
            string desiredBalance = Console.ReadLine();
            bool canConvertBalance = double.TryParse(desiredBalance, out convertedBalance);
            if (canConvertBalance)
            {
                convertedBalance = double.Parse(desiredBalance);
                isValid |= convertedBalance > 0;
                return (isValid, convertedBalance);

            }
            else
            {
                return (isValid, -1000);
            }
        }

        public (bool, double) ValidStartingBalance(string MessageString)
        {
            // Ensures Starting Balance is valid
            //Parameters:
            // MessageString   - Message that displays kind of account
            Console.WriteLine(MessageString);
            Console.WriteLine("How much do you want to start the account with?");
            return ValidateAmount();
            
        }

        public (bool, double) ValidPrincipal(string MessageString)
        {
            // Ensures principal used in transactions is valid
            //Parameters:
            // MessageString   - Message that displays kind of transaction
            Console.WriteLine(MessageString);
            Console.WriteLine("In what amount do you want to perform this transaction?");
            return ValidateAmount();
        }

        public (bool, string) ValidName()
        {
            // Ensures Input string has no numbers
            string Name = Console.ReadLine();
            bool IsValid = false;
            bool containsInt = Name.Any(c => char.IsDigit(c));
            if (containsInt)
            {
                Console.WriteLine("Name is invalid");
                return (IsValid, "InValidName");
            }
            else
            {
                IsValid = true;
                return (IsValid, Name);
            }
        }

        public (bool, int) ValidNumber()
        {
            // Ensures Input number is valid
            string Number = Console.ReadLine();
            bool isInt = Number.All(c => char.IsDigit(c));
            if (isInt)
            {
                bool canConvert = int.TryParse(Number, out int ConvertedNumber);
                if (canConvert)
                {
                    ConvertedNumber = int.Parse(Number);
                    return (isInt, ConvertedNumber);
                }
                else
                {
                    return (false, -1000);
                }
            }
            else
            {
                return (isInt, -1000);
            }
        }

        public (bool, int) ValidYear()
        {
            // Ensures Input number is valid
            string Number = Console.ReadLine();
            if (Number.Length == 4)
            {
                bool isInt = Number.All(c => char.IsDigit(c));
                if (isInt)
                {
                    bool canConvert = int.TryParse(Number, out int ConvertedNumber);
                    if (canConvert)
                    {
                        ConvertedNumber = int.Parse(Number);
                        if(ConvertedNumber > 0 && ConvertedNumber >= DateTime.Now.Year)
                            return (isInt, ConvertedNumber);
                        else
                            return (false, -1000);
                    }
                    else
                    {
                        return (false, -1000);
                    }
                }
                else
                {
                    return (isInt, -1000);
                }
            }
            else
            {
                return (false, -1000);
            }
            
        }

        public (bool, int) ValidMonth()
        {
            // Ensures Input number is valid
            string Number = Console.ReadLine();
            if (Number.Length <= 2)
            {
                bool isInt = Number.All(c => char.IsDigit(c));
                if (isInt)
                {
                    bool canConvert = int.TryParse(Number, out int ConvertedNumber);
                    if (canConvert)
                    {
                        ConvertedNumber = int.Parse(Number);
                        if (ConvertedNumber > 0 && ConvertedNumber < 13)
                            return (isInt, ConvertedNumber);
                        else
                            return (false, -1000);
                    }
                    else
                    {
                        return (false, -1000);
                    }
                }
                else
                {
                    return (isInt, -1000);
                }
            }
            else
            {
                return (false, -1000);
            }

        }

        public (bool, int) ValidDay()
        {
            // Ensures Input number is valid
            string Number = Console.ReadLine();
            if (Number.Length <= 2)
            {
                bool isInt = Number.All(c => char.IsDigit(c));
                if (isInt)
                {
                    bool canConvert = int.TryParse(Number, out int ConvertedNumber);
                    if (canConvert)
                    {
                        ConvertedNumber = int.Parse(Number);
                        if (ConvertedNumber > 0 && ConvertedNumber < 32)
                            return (isInt, ConvertedNumber);
                        else
                            return (false, -1000);
                    }
                    else
                    {
                        return (false, -1000);
                    }
                }
                else
                {
                    return (isInt, -1000);
                }
            }
            else
            {
                return (false, -1000);
            }

        }

        public (bool, string) ValidLoginInfo()
        {
            string LoginInfo = Console.ReadLine();
            bool validLength = (LoginInfo.Length > 0);
            if (validLength)
            {
                return (validLength, LoginInfo);
            }
            else
            {
                return (validLength, "InvalidLoginInfo");
            }
        }

        // Checks current standing of user
        // Change to only 
        //public bool StandingCheck(User user)
        //{
        //    bool GoodStanding = false;
        //    return GoodStanding;
        //}
    }
}