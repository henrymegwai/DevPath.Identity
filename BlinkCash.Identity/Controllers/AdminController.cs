using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using BlinkCash.Core.Models.AuthModels;
using BlinkCash.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthManager _authMgr;
        private readonly IPlanService _planService;
        public AdminController(IAuthManager authMgr, IPlanService planService)
        {
            _authMgr = authMgr;
            _planService = planService;
        }


        [HttpPost("/create/user")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel model)
        {
            ExecutionResponse<CreateUserResponse> response = new ExecutionResponse<CreateUserResponse>();

            try
            {
                model.Validate();
                response = await _authMgr.CreateBackEndUser(model, model.Role);
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


        [HttpGet("Users/{page}/{pagesize}")]
        public async Task<IActionResult> GetUsers(Role roleType, string email, int page = 1, int pagesize = 20)
        {
            ExecutionResponse<User[]> response = new ExecutionResponse<User[]>();
            
            try
            {
                response = await _authMgr.GetUsers(roleType, page, pagesize);
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
       

        [HttpPost("Plans")]
        public async Task<IActionResult> GetPlans(PlanSearchVm model)
        {
            try
            {
                var result = await _planService.GetPlans(model);
                if (!result.Status)
                    BadRequest(result);
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