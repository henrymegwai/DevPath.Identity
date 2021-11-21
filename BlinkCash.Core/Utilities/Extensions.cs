using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Utilities
{
    public static class Extensions
    {
        public static string GenerateTnxReference(this Guid value)
        {
            ShortGuid shortguid = value;
            return shortguid; //Constants.TransactionRefCode + shortguid.ToString().Replace('-', '0').Replace('_', '0').ToUpper();
        }

        public static T ToEnum<T>(this string value) where T : struct
        {
            if (Enum.TryParse(value, out T result))
            {
                return result;
            }
            else
            {
                return default(T);
            }
        }


        public static string PasswordGenerateCode(string email)
        {
            // define characters allowed in passcode.  set length so divisible into 256
            char[] ValidChars = {'2','3','4','5','6','7','8','9','2','3','4','5','6','7','8','9','2','3','4','5','6','7','8','9','2','3','4','5','6','7','8','9',
                   'A','B','C','D','E','F','G','H',
                   'J','K','L','M','N','P','Q',
                   'R','S','T','U','V','W','X','Y','Z'}; // len=32

            const string hashkey = "blinkcash"; //key for HMAC function -- change!
            const int codelength = 8; // lenth of passcode
            string code = GetCodeForEmail(email);
            return code;
            string GetCodeForEmail(string address)
            {
                byte[] hash;
                using (HMACSHA1 sha1 = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(hashkey)))
                    hash = sha1.ComputeHash(UTF8Encoding.UTF8.GetBytes(address));
                int startpos = hash[hash.Length - 1] % (hash.Length - codelength);
                StringBuilder passbuilder = new StringBuilder();
                for (int i = startpos; i < startpos + codelength; i++)
                    passbuilder.Append(ValidChars[hash[i] % ValidChars.Length]);
                return passbuilder.ToString();
            }
        }

        public static DateTime ToDatetime(this string value)
        {
            DateTime dt = default(DateTime);
            if (string.IsNullOrEmpty(value))
                return dt;

            DateTime.TryParseExact(value, "dd-MM-yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out dt);

            return dt;
        }
        public static DateTime ToDateTime(this string value, string dateFormat, int FormatTimeType)
        {
            DateTime dt = default(DateTime);
            if (DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out dt))
            {
                if (FormatTimeType == 1)
                {
                    dt = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 0);
                }
                else
                {
                    dt.AddHours(0);
                    dt.AddMinutes(1);
                }
                return dt;
            }
            return default(DateTime);
        }

        public static string ToSha256(this string rawValue)
        {
            //return rawValue;
            if (string.IsNullOrEmpty(rawValue))
            {
                return null;
            }
            using (var alg = SHA256.Create())
            {
                var bytes = alg.ComputeHash(Encoding.UTF8.GetBytes(rawValue));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateToken(this string value, int count, char ch)
        {
            string s = new string($"{Guid.NewGuid().ToString()}{Guid.NewGuid().ToString()}".Where(x => char.IsDigit(x)).ToArray());
            s = $"{value}{s}";
            if (s.Length >= count)
                return s.Substring(0, count);
            return s.PadRight(count, ch);
        }

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
