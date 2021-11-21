using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IPinService
    {
        Task<ExecutionResponse<PinVerificationResponse>> VerifyPin(TransactionPinViewModel model);
        Task<ExecutionResponse<string>> CreatePin(TransactionPinCreateViewModel model);
        Task<ExecutionResponse<string>> ChangePin(TransactionPinUpdateViewModel model);
        Task<ExecutionResponse<string>> ResetPin(TransactionPinResetViewModel model);
        
    }
}
