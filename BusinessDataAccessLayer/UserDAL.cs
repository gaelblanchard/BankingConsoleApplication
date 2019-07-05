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
        //Follows a GET-POST-UPDATE-DELETE design pattern
        //GET overloading for any other qualities
        public User GetUserDAL(int givenUserID)
        {
            //Parameters:
            // givenUserID   - Given user ID
            int UserIndex = BankingSystem.Users.FindIndex(n => n.UserId == givenUserID);
            if(UserIndex > 0)
            {
                return BankingSystem.Users[UserIndex];
            }
            else
            {
                return null;
            }
        }

        public User GetUserDAL(string givenUserName,string givenPassword)
        {
            //Parameters:
            // givenUserName   - Given userName
            // givenPassword   - Given password
            int UserIndex = BankingSystem.Users.FindIndex(n => n.UserName == givenUserName && n.Password==givenPassword);
            if (UserIndex > 0)
            {
                return BankingSystem.Users[UserIndex];
            }
            else
            {
                return null;
            }
        }
        //POST
        public void RegisterUserDAL(User user)
        {
            //Parameters:
            // user   - Given User object to register
            BankingSystem.Users.Add(user);
        }

        //UPDATE
        public void UpdateUserDAL(User updatedUser)
        {
            //Parameters:
            // updatedUser   - Given User object to update
            int UserIndex = BankingSystem.Users.FindIndex(n => n.UserId == updatedUser.UserId);
            BankingSystem.Users[UserIndex] = updatedUser;
        }

        //DELETE
        public void DeleteUserDAL(int givenUserId)
        {
            //Parameters:
            // user   - Given User object to delete
            int UserIndex = BankingSystem.Users.FindIndex(n => n.UserId == givenUserId);
            BankingSystem.Users.RemoveAt(UserIndex);
        }

    }

}
