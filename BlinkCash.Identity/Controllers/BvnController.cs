using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Models.BvnModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlinkCash.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BvnController : ControllerBase
    {
       
        private readonly IAuthManager _authManager;
        private readonly ILogger<BvnController> _logger;
        public BvnController( IAuthManager authManager, ILogger<BvnController> logger)
        {
             
            _authManager = authManager;
            _logger = logger;
        }


        [HttpGet("verify/{bvn}")]
        public IActionResult GetBVN(string bvn)
        {

            ExecutionResponse<BvnResponse> response = new ExecutionResponse<BvnResponse>();
            var newbvn = _authManager.GenerateBVN();
            response.Data = newbvn;
           response.Status = true;
            response.Message = "Bvn details returned successfully";
            return Ok(response);
        }

        
    }
}
