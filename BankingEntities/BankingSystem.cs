using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    public static class BankingSystem
    {
        // Public Static Class which contains all users
        //in banking system
        public static List<User> Users { get; set; }
        static BankingSystem()
        {
            Users = new List<User>();
        }
    }
}
