using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IUtilityService
    {
        
        string GetTransactionRef();
        DateTime TimeZoneConverter(DateTime date);
        string HMAC_SHA256(string toHash, string key);
        string UserId();
        string UserName();
        string GetUserToken();
        List<string> GetUserRole();
        string ComputeHash(string plainText);
    }
}
