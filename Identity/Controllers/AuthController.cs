using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlinkCash.Identity.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtManager _jwtManager;
        private readonly IAuthManager _authManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IJwtManager jwtManager, IAuthManager authManager, ILogger<AuthController> logger)
        {
            _jwtManager = jwtManager;
            _authManager = authManager;
            _logger = logger;
        } 

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestModel model)
        {
            try
            {
               // model.Validate();
                var createUserAccount = await _authManager.CreateUser(model);
                if(!createUserAccount.Status)
                    return BadRequest(createUserAccount);

                return Ok(createUserAccount);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [AllowAnonymous]
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestModel model)
        {
            try
            {
                model.Validate();
                var forgotUser = await _authManager.ForgotPassword(model);
                if (!forgotUser.Status)
                    return BadRequest(forgotUser);

                return Ok(forgotUser);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                model.Validate();
                var forgotUser = await _authManager.ResetPassword(model);
                if (!forgotUser.Status)
                    return BadRequest(forgotUser);

                return Ok(forgotUser);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                model.Validate();
                var forgotUser = await _authManager.ChangePassword(model);
                if (!forgotUser.Status)
                    return BadRequest(forgotUser);

                return Ok(forgotUser);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [AllowAnonymous]
        [HttpPost("checkphonenumber")]
        public async Task<IActionResult> CheckPhone(CheckPhoneNumberRequestModel model)
        {
            try
            {
                model.Validate();
                var phoneNumberExist = await Task.Run(()=>_authManager.CheckPhoneNumber(model));
                return Ok(phoneNumberExist);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }



        
    }
}
