using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using BlinkCash.Core.Models.RequestModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class SecurityQuestionManager : ISecurityQuestionManager
    {
        private readonly ISecurityQuestionRepository _securityQuestionRepository;
        private readonly IUtilityService _utilityService;
        private readonly IResponseService _responseService;
        private readonly UserManager<IdentityUserExtension> _userManager;

        public SecurityQuestionManager(IResponseService responseService, ISecurityQuestionRepository bankRepository, IUtilityService utilityService, UserManager<IdentityUserExtension> userManager)
        {
            _securityQuestionRepository = bankRepository;
            _responseService = responseService;
            _utilityService = utilityService;
            _userManager = userManager;
        }

        public async Task<ExecutionResponse<SecurityQuestionDto>> CreateSecurityQuestion(SecurityQuestionDto securityQuestionDto)
        {
            if (securityQuestionDto == null)
                return _responseService.ExecutionResponse<SecurityQuestionDto>("invalid request");

            if (string.IsNullOrWhiteSpace(securityQuestionDto.Question))
                return _responseService.ExecutionResponse<SecurityQuestionDto>("Security Question must be supplied");

            var sql = @"SELECT Question from SecurityQuestion where Question = @question and IsDeleted = 0";

            var bankCheck = _securityQuestionRepository.DapperSqlWithParams<SecurityQuestionDto>(sql, new { question = securityQuestionDto.Question }, null).Any();

            if (bankCheck)
                return _responseService.ExecutionResponse<SecurityQuestionDto>("Security Question already exist");

            var result = await _securityQuestionRepository.CreateSecurityQuestion(securityQuestionDto);
            return _responseService.ExecutionResponse<SecurityQuestionDto>("successfully created Security Question", result, true);
        }

        public async Task<ExecutionResponse<string>> DeleteSecurityQuestion(long Id)
        {
            var securityQuestionDto = await _securityQuestionRepository.DeleteSecurityQuestion(Id);
            if (!securityQuestionDto)
                return _responseService.ExecutionResponse<string>("Security Question was not deleted successfully", null, false);

            return _responseService.ExecutionResponse<string>("Security Question was not deleted successfully", null, false);

        }

        public async Task<ExecutionResponse<SecurityQuestionDto>> GetSecurityQuestion(long Id)
        {
            var bank = await _securityQuestionRepository.GetSecurityQuestion(Id);
            if (bank == null)
                return _responseService.ExecutionResponse<SecurityQuestionDto>("Security Question does not exist");

            return _responseService.ExecutionResponse<SecurityQuestionDto>("successfully retrieved Security Question", bank, true);
        }

        public async Task<ExecutionResponse<SecurityQuestionDto[]>> GetSecurityQuestions()
        {
            var securityQuests = await _securityQuestionRepository.GetSecurityQuestions();

            securityQuests = securityQuests.OrderBy(x => x.CreatedDate).ToArray();
            return _responseService.ExecutionResponseList("Successfully retrieved Security Questions", securityQuests, true);
        }

        public async Task<ExecutionResponse<SecurityQuestionDto>> UpdateSecurityQuestion(SecurityQuestionDto securityQuestionDto, long id)
        {
            if (securityQuestionDto == null)
                return _responseService.ExecutionResponse<SecurityQuestionDto>("invalid request");

            if (string.IsNullOrWhiteSpace(securityQuestionDto.Question))
                return _responseService.ExecutionResponse<SecurityQuestionDto>("Security Question is required");

            var targetBank = await _securityQuestionRepository.GetSecurityQuestion(id);
            if (targetBank == null)
                return _responseService.ExecutionResponse<SecurityQuestionDto>("Security Question does not exist");
            securityQuestionDto.ModifiedBy = _utilityService.UserName();
            var result = await _securityQuestionRepository.UpdateSecurityQuestion(securityQuestionDto, id);
            return _responseService.ExecutionResponse<SecurityQuestionDto>("successfully updated bank", null, true);
        }

        public async Task<ExecutionResponse<SecurityQuestionDto>> GetSecurityQuestionByName(string name)
        {
            var bank = await _securityQuestionRepository.GetSecurityQuestionByName(name);

            return _responseService.ExecutionResponse<SecurityQuestionDto>("successfully retrieved Security Question", bank, true);
        }


        public async Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> CreateUpdateUserSecurityQuestionAndAnswer(UserSecurityQuestionAndAnswerDto securityQuestionDto)
        {
            if (securityQuestionDto == null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(securityQuestionDto.Answer))
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("Answer must be supplied", null, false);
            string UserId = _utilityService.UserId();

            var sql = @"SELECT SecurityQuestionId from UserSecurityQuestionAndAnswer where SecurityQuestionId = @securityQuestionId and  UserId = @userId";
            

            var bankCheck = _securityQuestionRepository.DapperSqlWithParams<UserSecurityQuestionAndAnswerResponse>(sql, new { securityQuestionId = securityQuestionDto.SecurityQuestionId, userId = UserId }, null).Any();

            if (bankCheck)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("User Security Question has already been set up", new UserSecurityQuestionAndAnswerResponse { SecurityQuestionId = securityQuestionDto.SecurityQuestionId, Answer = securityQuestionDto.Answer }, false );

            securityQuestionDto.UserId = UserId;
            var result = await _securityQuestionRepository.CreateUserSecurityQuestionAndAnswer(securityQuestionDto);
            return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("successfully created User Security Question Answer", new UserSecurityQuestionAndAnswerResponse { SecurityQuestionId = securityQuestionDto.SecurityQuestionId,  Answer = securityQuestionDto.Answer }, true);
        }


        public async Task<ExecutionResponse<UserSecurityQuestionAndAnswerDto>> UpdateUserSecurityQuestionAndAnswerDto(UserSecurityQuestionAndAnswerDto securityQuestionDto)
        {
            if (securityQuestionDto == null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerDto>("invalid request", null, false);

            if (string.IsNullOrWhiteSpace(securityQuestionDto.Answer))
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerDto>("User Security Question Answer is required", null, false);
            string UserId = _utilityService.UserId();
            var targetBank = await _securityQuestionRepository.GetUserSecurityQuestionAndAnswer(securityQuestionDto.SecurityQuestionId, UserId) ;
            if (targetBank == null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerDto>("User Security Question  does not exist for this user. Please create one.");
            securityQuestionDto.UserId = UserId;
            securityQuestionDto.ModifiedBy = _utilityService.UserName();
            var result = await _securityQuestionRepository.UpdateUserSecurityQuestionAndAnswer(securityQuestionDto);
            return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerDto>("Successfully updated User Security Question And Answer", null, true);
        }

        public async Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> GetUserSecurityQuestionAndAnswerId(long SecurityQuestionId)
        {
            string UserId = _utilityService.UserId();
            var bank = await _securityQuestionRepository.GetUserSecurityQuestionAndAnswerId(UserId, SecurityQuestionId);
            if (bank != null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("successfully retrieved User Security Question Anwser", new UserSecurityQuestionAndAnswerResponse { SecurityQuestionId = bank.SecurityQuestionId, Answer = bank.Answer, SecurityQuestion = bank.SecurityQuestion.Question }, true);

            return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("No Answer to any Security Questions", null, false);
        
        }

        public async Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> GetUserSecurityQuestionAndAnswerByUserId()
        {
            string UserId = _utilityService.UserId();
            var bank = await _securityQuestionRepository.GetUserSecurityQuestionAndAnswerByUserId(UserId);
            if(bank != null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("successfully retrieved User Security Question Anwser", new UserSecurityQuestionAndAnswerResponse { SecurityQuestionId = bank.SecurityQuestionId, Answer = bank.Answer, SecurityQuestion = bank.SecurityQuestion.Question }, true);

            return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("No Answer to any Security Questions", null, false);
        }
        public async Task<ExecutionResponse<UserSecurityQuestionAndAnswerResponse>> GetUserSecurityQuestionFoPhoneNumber(string phonenumber)
        {
            var userExist = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phonenumber);
            if(userExist == null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("Phone number does not exist", null, false);
        
            var bank = await _securityQuestionRepository.GetUserSecurityQuestionAndAnswerByUserId(userExist.Id);
            if(bank != null)
                return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("successfully retrieved User Security Question Anwser", new UserSecurityQuestionAndAnswerResponse { SecurityQuestionId = bank.SecurityQuestionId, Answer = bank.Answer, SecurityQuestion = bank.SecurityQuestion.Question }, true);

            return _responseService.ExecutionResponse<UserSecurityQuestionAndAnswerResponse>("No Answer to any Security Questions", null, false);
        }

        public async Task<ExecutionResponse<VerifyAnswerResponse>> VerifyUserAnswer(VerifyAnswerRequest model)
        {
            var userExist = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
            if (userExist == null)
                return _responseService.ExecutionResponse<VerifyAnswerResponse>("Phone number does not exist", null, false);

            var bank = await _securityQuestionRepository.GetUserSecurityQuestionAndAnswerByUserId(userExist.Id);
            if (bank == null)
            {
                return _responseService.ExecutionResponse<VerifyAnswerResponse>("No Secutiry Question or Answer was found for this phone number", null, false);
            }
            var validAnswer = bank.Answer.ToLower() == model.Answer.ToLower() ? true : false;
            return _responseService.ExecutionResponse<VerifyAnswerResponse>("Successfully verified user answer", new VerifyAnswerResponse {  DoesAnswerMatch = validAnswer, Answer = bank.Answer, SecurityQuestion = bank.SecurityQuestion.Question }, true);

            
        }
    }
}
