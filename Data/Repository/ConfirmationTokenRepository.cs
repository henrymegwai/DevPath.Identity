using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Data.Entities;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class ConfirmationTokenRepository : IConfirmationTokenRepository
    {
        private readonly AppDbContext _dbContext;
        public ConfirmationTokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ConfirmationTokenDto> Create(ConfirmationTokenDto model)
        {
            var entity = model.Map();

            _dbContext.Set<ConfirmationToken>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity.Map();
        }

        public async Task<bool> Deactivate(string TokenId)
        {
            var entity = await _dbContext.Set<ConfirmationToken>().FirstOrDefaultAsync(x => x.TokenId == TokenId);
            if (entity == null)
                return false;

            entity.IsDeleted = true;

            return true;
        }

        public async Task<ConfirmationTokenDto> Get(string TokenId)
        {
            var entity = await _dbContext.Set<ConfirmationToken>().FirstOrDefaultAsync(x => x.TokenId == TokenId);
            if (entity == null)
                return null;

            return entity.Map();
        }
    }
}
