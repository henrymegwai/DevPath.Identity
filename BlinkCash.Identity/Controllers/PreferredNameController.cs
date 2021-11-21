using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models.AuthModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Identity.Controllers
{
    public class PreferredNameController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public PreferredNameController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

 
        [HttpPut("update")]
        public async Task<IActionResult> ChangePin(UpdatePreferredNameRequest model)
        {
            ExecutionResponse<UpdatePreferredNameResponse> response = new ExecutionResponse<UpdatePreferredNameResponse>();
            try
            {
                model.Validate();
                response = await _authManager.UpdatePreferedName(model);
                if (!response.Status)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
                return BadRequest(response);

            }
        }
    }
}
