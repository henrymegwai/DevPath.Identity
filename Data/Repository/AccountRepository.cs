using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<AccountDto> CreateAccount(AccountDto model)
        {
            Account account = model.Map();
            _context.Set<Account>().Add(account);
            await _context.SaveChangesAsync();
            return account.Map();
        }

        public async Task<AccountDto> GetAccount(string UserId)
        {              
            Account account = await _context.Set<Account>().FirstOrDefaultAsync(x=>x.UserId == UserId);
            if (account == null)
                return null;

            return account.Map();
        }

        public async Task<AccountDto> GetAccountById(string accountId)
        {
            Account account = await _context.Set<Account>().FirstOrDefaultAsync(x => x.AccountId == accountId);
            if (account == null)
                return null;

            return account.Map();
        }

        public async Task<AccountDto> UpdateAccount(AccountDto model, long Id)
        {
            var entity = await _context.Set<Account>().FindAsync(Id);

            if (entity == null)
                return null;
            entity.TransactionPin = model.TransactionPin;
            entity.HasTransactionPin = model.HasTransactionPin; 
            entity.UpdateFlag = model.UpdateFlag;
            entity.IsTransactionPinHashed = model.IsTransactionPinHashed;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ModifiedBy;
            _context.Entry<Account>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity.Map(); 
        }

        public async Task<AccountDto> UpdateAccountWithHasRequestedCard(string accountId, bool hasRequestedCard,string UpdatedBy)
        {
            var entity = await _context.Set<Account>().FirstOrDefaultAsync(x=>x.AccountId == accountId);

            if (entity == null)
                return null;
            entity.HasRequestedCard = hasRequestedCard; 
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = UpdatedBy;
            _context.Entry<Account>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity.Map();
        }
    }
}
