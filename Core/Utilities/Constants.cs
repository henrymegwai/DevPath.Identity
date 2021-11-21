using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Utilities
{

    public static class Constants
    {
        public const string TransactionRefCode = "BLC";

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }

    public static class NotificationConstants
    {
         
        public static string Account_Creation = "Blink Cash Account Creation";
        public static string Account_Backend_User = "Blink Cash User Creation";
        public static string Password_Reset = "Password Reset";
        public static string Forgot_Password = "Forgot Password";
    }

}
