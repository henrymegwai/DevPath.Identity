using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Identity.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public UserController( IAuthManager authManager)
        {            
            _authManager = authManager;            
        }

        
        [HttpPatch("{phoneNumber}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestModel model, string phoneNumber)
        {
            try
            {
                // model.Validate();
                var updateUserAccount = await _authManager.UpdateUser(model, phoneNumber);
                if (!updateUserAccount.Status)
                    return BadRequest(updateUserAccount);

                return Ok(updateUserAccount);
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                ExecutionResponse<string> response = new ExecutionResponse<string>();
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPatch("frequentlydialedNumbers/{phoneNumber}")]
        public async Task<IActionResult> UpdateFrequentlyDialedNumber([FromBody] FrequentlyDialedNumberRequestModel model, string phoneNumber)
        {
            try
            {
                // model.Validate();
                var updateUserAccount = await _authManager.UpdateFrequentlyDialedNumber(model, phoneNumber);
                if (!updateUserAccount.Status)
                    return BadRequest(updateUserAccount);

                return Ok(updateUserAccount);
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
