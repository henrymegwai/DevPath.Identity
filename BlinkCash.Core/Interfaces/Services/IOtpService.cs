using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models.NubanModels;
using BlinkCash.Core.Models.OtpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IOtpService
    {
        Task<ExecutionResponse<OtpResponse>> Send(OtpRequest model);
        Task<ExecutionResponse<OtpResponse>> Verify(OtpRequest model);
    }
}
