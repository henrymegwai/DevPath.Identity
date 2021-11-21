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

namespace BlinkCash.Identity.Controllers
{
    [Route("api/auth/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtManager _jwtManager;
        private readonly IAuthManager _authManager;
        private readonly ILogger<AuthController> _logger;
        public LoginController(IJwtManager jwtManager, IAuthManager authManager, ILogger<AuthController> logger)
        {
            _jwtManager = jwtManager;
            _authManager = authManager;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("withemail")]
        public async Task<IActionResult> LoginWithEmailRequest([FromBody] LoginWithEmailRequest request)
        {
            try
            {
                ExecutionResponse<LoginResult> response = new ExecutionResponse<LoginResult>();
                request.Validate();
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "Invalid field(s)";
                    response.Data = null;
                    return BadRequest(response);
                }
                var validateuser = await _authManager.IsValidUserCredentialsWithoutPassowrd(request.Email);
                if (!validateuser.Isvalid)
                {
                    response.Status = false;
                    response.Message = validateuser.message;
                    response.Data = null;
                    return BadRequest(response);
                }

                var roleResult = await _authManager.GetUserRole(validateuser.identityUserExtension.Email);
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name,validateuser.identityUserExtension.UserName),
                     new Claim(ClaimTypes.NameIdentifier,validateuser.identityUserExtension.Id)
                };
                var roles = roleResult.role.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
                claims.AddRange(roles);

                var jwtResult = _jwtManager.GenerateTokens(request.Email, claims.ToArray(), DateTime.Now);
                _logger.LogInformation($"User [{request.Email}] logged in the system.");

               
                response.Data = new LoginResult
                {
                    UserName = request.Email,
                    Role = roleResult.role,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                    User = validateuser.identityUserExtension
                };
                response.Status = true;
                response.Message = "Successful";
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                response.Data = null;
                response.Status = false;
                return BadRequest(response);
            }
        }
        
        [AllowAnonymous]
        [HttpPost("withphonenumber")]
        public async Task<IActionResult> LoginWithPhoneNumberRequest([FromBody] LoginWithPhoneNumberRequest request)
        {
            try
            {
                ExecutionResponse<LoginResult> response = new ExecutionResponse<LoginResult>();
                request.Validate();
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "Invalid field(s)";
                    response.Data = null;
                    return BadRequest(response);
                }
                var validateuser = await _authManager.IsValidUserCredentialsWithPhoneNumber(request.PhoneNumber);
                if (!validateuser.Isvalid)
                {
                    response.Status = false;
                    response.Message = validateuser.message;
                    response.Data = null;
                    return BadRequest(response);
                }

                var roleResult = await _authManager.GetUserRole(validateuser.identityUserExtension.UserName);
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name,validateuser.identityUserExtension.UserName),
                     new Claim(ClaimTypes.NameIdentifier,validateuser.identityUserExtension.Id)
                };
                var roles = roleResult.role.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
                claims.AddRange(roles);

                var jwtResult = _jwtManager.GenerateTokens(validateuser.identityUserExtension.UserName, claims.ToArray(), DateTime.Now);
                _logger.LogInformation($"User [{request.PhoneNumber}] logged in the system.");

             
                response.Data = new LoginResult
                {
                    UserName = validateuser.identityUserExtension.UserName,
                    Role = roleResult.role,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,
                    User = validateuser.identityUserExtension
                };
                response.Status = true;
                response.Message = "Successful";
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                response.Status = true;
                return BadRequest(response);
            }
        }
        [AllowAnonymous]
        [HttpPost("withemail/password")]
        public async Task<IActionResult> LoginWIthEmailAndPasword([FromBody] LoginWithEmailPasswordRequest request)
        {
            try
            {
                ExecutionResponse<LoginResult> response = new ExecutionResponse<LoginResult>();
                request.Validate();
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "Invalid field(s)";
                    response.Data = null;
                    return BadRequest(response);
                }
                var validateuser = await _authManager.IsValidUserCredentials(request.UserName, request.Password);
                if (!validateuser.Isvalid)
                {
                    response.Status = false;
                    response.Message = validateuser.message;
                    response.Data = null;
                    return BadRequest(response); 
                }

                var roleResult = await _authManager.GetUserRole(validateuser.identityUserExtension.Email);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,validateuser.identityUserExtension.UserName),
                     new Claim(ClaimTypes.NameIdentifier,validateuser.identityUserExtension.Id)
                };

                var roles = roleResult.role.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
                claims.AddRange(roles);
                var jwtResult = _jwtManager.GenerateTokens(request.UserName, claims.ToArray(), DateTime.Now);
                _logger.LogInformation($"User [{request.UserName}] logged in the system.");

                
                response.Data = new LoginResult
                {
                    UserName = request.UserName,
                    Role = roleResult.role,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString,User = validateuser.identityUserExtension
                };
                response.Status = true;
                response.Message = "Successful";
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                response.Status = false;
                response.Data = null;
                return BadRequest(response);
            }
        }



        [AllowAnonymous]
        [HttpPost("withphonenumber/password")]
        public async Task<IActionResult> LoginWIthPhoneNumberAndPasword([FromBody] LoginWithPhonePasswordRequest request)
        {
            try
            {
                ExecutionResponse<LoginResult> response = new ExecutionResponse<LoginResult>();
                request.Validate();
                if (!ModelState.IsValid)
                {
                    response.Status = false;
                    response.Message = "Successful";
                    return BadRequest(response);
                }
                var validateuser = await _authManager.IsValidUserCredentialsWithPhoneNumber(request.PhoneNumber, request.Password);
                if (!validateuser.Isvalid)
                {
                    response.Status = false;
                    response.Message = validateuser.message;
                    response.Data = null;
                    return BadRequest(response);
                }

                var roleResult = await _authManager.GetUserRole(validateuser.identityUserExtension.Email);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,validateuser.identityUserExtension.UserName),
                     new Claim(ClaimTypes.NameIdentifier,validateuser.identityUserExtension.Id)
                };

                var roles = roleResult.role.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
                claims.AddRange(roles);
                var jwtResult = _jwtManager.GenerateTokens(request.PhoneNumber, claims.ToArray(), DateTime.Now);
                _logger.LogInformation($"User [{request.PhoneNumber}] logged in the system.");

               
                response.Data = new LoginResult
                {
                    UserName = validateuser.identityUserExtension.UserName,
                    Role = roleResult.role,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString, User = validateuser.identityUserExtension
                };
                response.Status = true;
                response.Message = "Successful";
                return Ok(response);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                response.Status = false;
                return BadRequest(response);
            }
        }
    }
}
