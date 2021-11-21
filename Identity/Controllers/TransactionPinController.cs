using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models.RequestModels; 
using Microsoft.AspNetCore.Mvc;

namespace BlinkCash.Identity.Controllers 
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionPinController  : ControllerBase
    {
        private readonly IPinService _pinService;
        public TransactionPinController(IPinService pinService) 
        {
            _pinService = pinService;
        }
        //[HttpPost("VerifyPin")]
        //public async Task<IActionResult> VerifyPin(TransactionPinViewModel model)
        //{
        //    ExecutionResponse<PinVerificationResponse> response = new ExecutionResponse<PinVerificationResponse>();
        //    try
        //    {
        //        model.Validate(); 
        //        response = await _pinService.VerifyPin(model);
        //        if (!response.Status)
        //            return BadRequest(response);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Data = null;
        //        response.Status = false;
        //        response.Message = ex.Message;
        //        return BadRequest(response);
                
        //    }
        //}

        [HttpPost("CreatePin")]
        public async Task<IActionResult> CreatePin(TransactionPinCreateViewModel model)
        {
            ExecutionResponse<string> response = new ExecutionResponse<string>();
            try
            {
                model.Validate();

                response = await _pinService.CreatePin(model);
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
        [HttpPut("UpdatePin")]
        public async Task<IActionResult> ChangePin(TransactionPinUpdateViewModel model)
        {
            ExecutionResponse<string> response = new ExecutionResponse<string>();
            try
            {
                model.Validate();
                response = await _pinService.ChangePin(model);
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
        //[HttpPut("ResetPin")]
        //public async Task<IActionResult> ResetPin(TransactionPinResetViewModel model)
        //{
        //    ExecutionResponse<string> response = new ExecutionResponse<string>();
        //    try
        //    {
        //        model.Validate();
        //        response = await _pinService.ResetPin(model);
        //        if (!response.Status)
        //            return BadRequest(response);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Data = null;
        //        response.Status = false;
        //        response.Message = ex.Message;
        //        return BadRequest(response);
        //    }
        //}
    }
}
