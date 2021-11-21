using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models.NubanModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlinkCash.Identity.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly INubanService _nubanService;
        private readonly IAuthManager _authMgr;
        public AccountController(INubanService nubanService, IAuthManager authMgr)
        {
            _nubanService = nubanService;
            _authMgr = authMgr;
        }
        // GET api/<AccountController>/5
        [HttpGet("nubanbalance")]
        public async Task<IActionResult> GetNubanBalance()
        {
            ExecutionResponse<NubanBalanceResponse> response = new ExecutionResponse<NubanBalanceResponse>();

            try
            {
                var accountId = Request.Headers["accountId"];
                if (string.IsNullOrEmpty(accountId))
                {
                    response.Data = null;
                    response.Status = false;
                    response.Message = "Nuban is required *accountId*";
                    return BadRequest(response);
                }
                var result = await _nubanService.NubanBalance(new NubanBalanceRequest { Nuban = accountId });
                if (result.Status)
                {
                    result.Data.Nuban = accountId;
                    response.Data = result.Data;
                    response.Status = result.Status;
                    response.Message = result.Message;
                    return Ok(response);
                }
                response.Data = result.Data;
                response.Status = result.Status;
                response.Message = result.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("nuban")]
        public async Task<IActionResult> GetBalance()
        {
            ExecutionResponse<AccountDto> response = new ExecutionResponse<AccountDto>();
            try
            {
                var result = await _authMgr.GetAccount();
                if (result.Status)
                {
                    response.Data = result.Data;
                    response.Status = result.Status;
                    response.Message = result.Message;
                    return Ok(response);
                }
                response.Data = result.Data;
                response.Status = result.Status;
                response.Message = result.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpPost("/update/cardrequest/flag")]
        public async Task<IActionResult> UpdateAccountWithHasRequestedCard(string accountId, bool HasRequestedCard)
        {
            ExecutionResponse<string> response = new ExecutionResponse<string>();

            try
            {
                var result = await _authMgr.UpdateAccountWithHasRequestedCard(accountId, HasRequestedCard);
                if (result.Status)
                {
                    response.Data = result.Data;
                    response.Status = result.Status;
                    response.Message = result.Message;
                    return Ok(response);
                }
                response.Data = result.Data;
                response.Status = result.Status;
                response.Message = result.Message;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("interbank/transfer")]
        public async Task<IActionResult> InterbankTransfer(WithdrawalRequest model)
        {
            ExecutionResponse<WithdrawalResponse> response = new ExecutionResponse<WithdrawalResponse>();
            try
            {
                model.Validate();
                var result = await _nubanService.InterBankTransfer(model);
                if (result.Status)
                {
                    response.Data = result.Data;
                    response.Status = result.Status;
                    response.Message = result.Message;
                    return Ok(response);
                }
                response.Data = result.Data;
                response.Status = result.Status;
                response.Message = result.Message;
                return BadRequest(response);
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
