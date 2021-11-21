using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Data.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class SecurityQuestionRepository : ISecurityQuestionRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public SecurityQuestionRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("BlinkCashDbContext"));
            }
        }
        public async Task<SecurityQuestionDto> CreateSecurityQuestion(SecurityQuestionDto model)
        {
            SecurityQuestion bank = model.Map();
            _context.Set<SecurityQuestion>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

        

        
        public async Task<bool> DeleteSecurityQuestion(long Id)
        {
            var entity = await _context.Set<SecurityQuestion>().FindAsync(Id);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SecurityQuestionDto> GetSecurityQuestion(long Id)
        {
            var entity = await _context.Set<SecurityQuestion>().FindAsync(Id);

            if (entity == null)
                return null;

            return entity.Map();
        }
         

        public async Task<SecurityQuestionDto> UpdateSecurityQuestion(SecurityQuestionDto model, long id)
        {
            var entity = await _context.Set<SecurityQuestion>().FindAsync(id);

            if (entity == null)
                return null;
            entity.Question = model.Question; 
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ModifiedBy;
            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public List<T> DapperSqlWithParams<T>(string sql, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql).ToList();
            }
        }

        public async Task<SecurityQuestionDto[]> GetSecurityQuestions()
        {
            var entity = await _context.Set<SecurityQuestion>().ToArrayAsync();
            if (entity == null)
                return null;

            return entity.Select(x => x.Map()).ToArray();
        }

        public List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null)
        {
            using (var connection = Connection)
            {
                return connection.Query<T>(sql, (object)parms).ToList();
            }
        }

        public async Task<SecurityQuestionDto> GetSecurityQuestionByName(string question)
        {
            var entity = await _context.Set<SecurityQuestion>().FirstOrDefaultAsync(x => x.Question.ToLower() == question.ToLower());

            if (entity == null)
                return null;

            return entity.Map();
        }




        public async Task<UserSecurityQuestionAndAnswerDto> CreateUserSecurityQuestionAndAnswer(UserSecurityQuestionAndAnswerDto model)
        {
            UserSecurityQuestionAndAnswer bank = model.Map();
            _context.Set<UserSecurityQuestionAndAnswer>().Add(bank);
            await _context.SaveChangesAsync();
            return bank.Map();
        }

        public async Task<UserSecurityQuestionAndAnswerDto> UpdateUserSecurityQuestionAndAnswer(UserSecurityQuestionAndAnswerDto model)
        {
            var entity = await _context.Set<UserSecurityQuestionAndAnswer>().FirstOrDefaultAsync(x => x.UserId == model.UserId && x.SecurityQuestionId == model.SecurityQuestionId);

            if (entity == null)
                return null;
            entity.Answer = model.Answer;
            entity.SecurityQuestionId = model.SecurityQuestionId;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedBy = model.ModifiedBy;
            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public async Task<UserSecurityQuestionAndAnswerDto> GetUserSecurityQuestionAndAnswer(long SecurityQuestionId, string UserId)
        {
            var entity = await _context.Set<UserSecurityQuestionAndAnswer>().FirstOrDefaultAsync(x=>x.UserId == UserId && x.SecurityQuestionId == SecurityQuestionId);

            if (entity == null)
                return null;

            return entity.Map();
        }


        public async Task<UserSecurityQuestionAndAnswerDto> GetUserSecurityQuestionAndAnswerId(string UserId, long Id)
        {
            var entity = await _context.Set<UserSecurityQuestionAndAnswer>().FirstOrDefaultAsync(x => x.SecurityQuestionId == Id && x.UserId == UserId);

            if (entity == null)
                return null;

            return entity.Map();
        }
        public async Task<UserSecurityQuestionAndAnswerDto> GetUserSecurityQuestionAndAnswerByUserId(string UserId)
        {
            var entity = await _context.Set<UserSecurityQuestionAndAnswer>().Include(c => c.SecurityQuestion).FirstOrDefaultAsync(x => x.UserId == UserId);

            if (entity == null)
                return null;

            return entity.Map();
        }

      
    }
}
