using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Services
{
    public interface IPlanService
    {
        Task<ExecutionResponse<object>> GetPlans(PlanSearchVm model);
    }
}
