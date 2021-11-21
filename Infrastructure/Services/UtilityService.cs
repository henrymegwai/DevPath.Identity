using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class UtilityService : IUtilityService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public UtilityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public string GetTransactionRef()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                ShortGuid shortguid = guid;
                var Reference = Constants.TransactionRefCode + shortguid.ToString().Replace('-', '0').Replace('_', '0').ToUpper();
                return Reference;
            }
            catch (Exception ex)
            {
                // Log.Error(ex);
                return null;
            }
        }

        public DateTime TimeZoneConverter(DateTime date)
        {
            try
            {
                TimeZoneInfo gmtTime = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime dateTime = TimeZoneInfo.ConvertTime(date, gmtTime);

                return dateTime;
            }
            catch
            {
                throw;
            }
        }

        public string HMAC_SHA256(string toHash, string key)
        {
            byte[] keyByte = Encoding.Default.GetBytes(key);
            using (HMACSHA256 sha256 = new HMACSHA256(keyByte))
            {
                Byte[] originalByte = Encoding.Default.GetBytes(toHash);
                Byte[] encodedByte = sha256.ComputeHash(originalByte);
                sha256.Clear();

                string hash = BitConverter.ToString(encodedByte).Replace("-", "").ToLower();
                return hash;
            }
        }

        public string UserId()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return userId;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);

                return "system";
            }

        }

        public string UserName()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                return userId;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return "system";
            }

        }

        public string GetUserToken()
        {
            var headers = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            string accessToken = headers[0];

            int tokenStartPos = accessToken.IndexOf("ey", StringComparison.OrdinalIgnoreCase);

            return accessToken.Substring(tokenStartPos, (accessToken.Length - tokenStartPos));
        }

        public List<string> GetUserRole()
        {
            try
            {
                var role = _httpContextAccessor.HttpContext.User.FindAll("role");
                return (role == null) ? new List<string> { "" } : role.Select(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                return new List<string> { "" };
            }
        }

        public string ComputeHash(string plainText)
        {
            SHA1 HashTool = new SHA1Managed();
            Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(plainText));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }
    }
}