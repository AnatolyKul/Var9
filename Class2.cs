using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Var_9
{
     class Class2
    {
        private static User profile;
        private static TradeEntities6 TradeEntities6;

        public static TradeEntities6 GetKulEntities()
        {
            if (TradeEntities6 == null)
            {
                TradeEntities6 = new TradeEntities6();
            }
            return TradeEntities6;
        }
        public static User GetUser()
        {
            return profile;
        }
        public static bool Authorize(string login, string Password)
        {
            string Phone = login.Trim();
            string pwd = Password.Trim();
            profile = GetKulEntities().User.Where(emp => emp.UserLogin == Phone && emp.UserPassword == pwd).FirstOrDefault();
            return profile != null;
        }
     }
}
