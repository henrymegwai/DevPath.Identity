using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface ISecurityQuestionRepository
    {
        Task<SecurityQuestionDto> CreateSecurityQuestion(SecurityQuestionDto model);
        Task<bool> DeleteSecurityQuestion(long Id);
        Task<SecurityQuestionDto> GetSecurityQuestion(long Id);
        Task<SecurityQuestionDto[]> GetSecurityQuestions();
        Task<SecurityQuestionDto> UpdateSecurityQuestion(SecurityQuestionDto model, long id);

        Task<SecurityQuestionDto> GetSecurityQuestionByName(string question);
        List<T> DapperSqlWithParams<T>(string sql, dynamic parms, string connectionnName = null);
        Task<UserSecurityQuestionAndAnswerDto> CreateUserSecurityQuestionAndAnswer(UserSecurityQuestionAndAnswerDto model);
        Task<UserSecurityQuestionAndAnswerDto> UpdateUserSecurityQuestionAndAnswer(UserSecurityQuestionAndAnswerDto model);
      
        Task<UserSecurityQuestionAndAnswerDto> GetUserSecurityQuestionAndAnswer(long SecurityQuestionId, string UserId);
        Task<UserSecurityQuestionAndAnswerDto> GetUserSecurityQuestionAndAnswerId(string UserId, long Id);
        Task<UserSecurityQuestionAndAnswerDto> GetUserSecurityQuestionAndAnswerByUserId(string UserId);
    }
}
