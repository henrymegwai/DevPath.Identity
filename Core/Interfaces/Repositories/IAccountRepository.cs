using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountDto> CreateAccount(AccountDto model);
        Task<AccountDto> UpdateAccount(AccountDto model, long Id);
        Task<AccountDto> GetAccount(string UserId);
        Task<AccountDto> UpdateAccountWithHasRequestedCard(string accountId, bool hasRequestedCard, string UpdatedBy);
        Task<AccountDto> GetAccountById(string accountId);
    }
}
