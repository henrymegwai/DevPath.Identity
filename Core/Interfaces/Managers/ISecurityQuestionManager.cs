using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface ISecurityQuestionManager
    {
        Task<ExecutionResponse<SecurityQuestionDto>> CreateSecurityQuestion(SecurityQuestionDto securityQuestionDto);
        Task<ExecutionResponse<SecurityQuestionDto>> UpdateSecurityQuestion(SecurityQuestionDto securityQuestionDto, long id);
        Task<ExecutionResponse<SecurityQuestionDto>> GetSecurityQuestion(long Id);
        Task<ExecutionResponse<SecurityQuestionDto[]>> GetSecurityQuestions();
        Task<ExecutionResponse<string>> DeleteSecurityQuestion(long Id);
        Task<ExecutionResponse<SecurityQuestionDto>> GetSecurityQuestionByName(string name);
        Task<ExecutionResponse<UserSecurityQuestionAndAnswerDto>> UpdateUserSecurityQuestionAndAnswerDto(UserSecurityQuestionAndAnswerDto securityQuestionDto);
        Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> CreateUpdateUserSecurityQuestionAndAnswer(UserSecurityQuestionAndAnswerDto securityQuestionDto);
        Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> GetUserSecurityQuestionAndAnswerId(long SecurityQuestionId);
        Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> GetUserSecurityQuestionAndAnswerByUserId();
        Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> GetUserSecurityQuestionFoPhoneNumber(string phonenumber);
        Task<ExecutionResponse<VerifyAnswerResponse>> VerifyUserAnswer(VerifyAnswerRequest model);
    }
}
