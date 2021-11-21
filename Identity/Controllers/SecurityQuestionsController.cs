using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityQuestionsController : Controller
    {
        private readonly ISecurityQuestionManager _securityQuestionManager;

        public SecurityQuestionsController(ISecurityQuestionManager securityQuestionManager)
        {
            _securityQuestionManager = securityQuestionManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bank = await _securityQuestionManager.GetSecurityQuestions();
                if (!bank.Status)
                    return BadRequest(bank);
                return Ok(bank);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromBody] SecurityQuestionRequest model, long id)
        {
            try
            {
                model.Validate();

                if (id <= default(long))
                    throw new Exception("Security Question Id is required");

                var payoutOption = await _securityQuestionManager.GetSecurityQuestion(id);
                if (payoutOption.Data == null)
                    throw new Exception("Security Question does not exist");

                var result = await _securityQuestionManager.UpdateSecurityQuestion(new SecurityQuestionDto
                {
                    Question = model.Question, 
                }, id);

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SecurityQuestionRequest model)
        {
            try
            {
                model.Validate();


                var payoutOption = await _securityQuestionManager.GetSecurityQuestionByName(model.Question);
                if (payoutOption.Data != null)
                    return BadRequest("Security Question already exist");


                var result = await _securityQuestionManager.CreateSecurityQuestion(new SecurityQuestionDto { Question = model.Question});

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= default(long))
                    throw new Exception("Security QuestionId is required");
                var result = await _securityQuestionManager.DeleteSecurityQuestion(id);
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                if (id <= default(long))
                    throw new Exception("id is required");

                var result = await _securityQuestionManager.GetSecurityQuestion(id);
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [Authorize]
        [HttpPatch("Update/Answer")]
        public async Task<IActionResult> UpdateUserSecurityQuestionAndAnswer([FromBody] UserSecurityQuestionAndAnswerRequest model)
        {
            try
            {
                model.Validate();
 
                var payoutOption = await _securityQuestionManager.GetUserSecurityQuestionAndAnswerId(model.SecurityQuestionId);
                if (payoutOption.Data == null)
                    throw new Exception("Security Question does not exist");

                var result = await _securityQuestionManager.UpdateUserSecurityQuestionAndAnswerDto(new UserSecurityQuestionAndAnswerDto
                {
                    Answer = model.Answer, SecurityQuestionId = model.SecurityQuestionId
                });

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);

            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [Authorize]
        [HttpPost("Create/Answer")]
        public async Task<IActionResult> CreateUserSecurityQuestionAndAnswer([FromBody] UserSecurityQuestionAndAnswerRequest model)
        {
            try
            {
                model.Validate();


                var payoutOption = await _securityQuestionManager.GetUserSecurityQuestionAndAnswerId(model.SecurityQuestionId);
                if (payoutOption.Data != null)
                    return BadRequest("User Security Question already exist for this user");


                var result = await _securityQuestionManager.CreateUpdateUserSecurityQuestionAndAnswer(new UserSecurityQuestionAndAnswerDto { SecurityQuestionId = model.SecurityQuestionId, Answer = model.Answer});

                if (!result.Status)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [Authorize]
        [HttpGet("Get/UserAnswer")]
        public async Task<IActionResult> GetUserSecurityQuestionAndAnswer()
        {
            try
            {
                

                var result = await _securityQuestionManager.GetUserSecurityQuestionAndAnswerByUserId();
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetSecurityQuestionFor/{phoneNumber}")]
        public async Task<IActionResult> GetUserSecurityQuestionAndAnswer(string phoneNumber)
        {
            try
            {
                var result = await _securityQuestionManager.GetUserSecurityQuestionFoPhoneNumber(phoneNumber);
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [AllowAnonymous]
        [HttpPost("VerifyAnswer")]
        public async Task<IActionResult> VerifyUserAnswer(VerifyAnswerRequest model)
        {
            try
            {
                var result = await _securityQuestionManager.VerifyUserAnswer(model);
                if (!result.Status)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
