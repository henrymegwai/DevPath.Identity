using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models.NubanModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface INubanService
    {
        Task<ExecutionResponse<NubanCreationResponse>> CreateNuban(NubanCreationRequest model); 
        Task<ExecutionResponse<NubanBalanceResponse>> NubanBalance(NubanBalanceRequest model);
        Task<ExecutionResponse<WithdrawalResponse>> InterBankTransfer(WithdrawalRequest model);
         
    }
}
