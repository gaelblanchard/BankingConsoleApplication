using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingEntities;

namespace BankingDataAccessLayer //For future Data Access operations
{
    public class UserDAL
    {
        public User RegisterUserDAL(User user)
        {
            //Create user using given specifications
            return user;
        }
        public User GetUser(User user)
        {
            //Get user
            return user;
        }
        public User UpdateUser(User user)
        {
            // updates user
            return user;
        }
        public IAccount CreateAccount(IAccount account)
        {
            //create Account
            return account;
        }
        public IAccount GetAccount(IAccount account)
        {
            // get account
            return account;
        }
        public IAccount UpdateAccount(IAccount account)
        {
            //update Account
            return account;
        }
        public void DeleteAccount(IAccount account)
        {
            //delete account
        }
        public Principal CreateTermDeposit(Principal principal)
        {
            //creates term deposit table
            return principal;
        }
        public Principal GetTermDeposit(Principal principal)
        {
            // Get Term Deposit
            return principal;
        }
        public Principal UpdateTermDeposit(Principal principal)
        {
            //update term deposit
            return principal;
        }
        public void DeleteTermDeposit(Principal principal)
        {
            //removes termdeposit
        }
    }
}
