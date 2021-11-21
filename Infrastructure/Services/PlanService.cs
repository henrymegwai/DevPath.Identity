using BlinkCash.Core.Configs;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class PlanService : IPlanService
    {

        private readonly IHttpService _httpService;
        private readonly AppSettings _appSettings;
        public PlanService(IOptions<AppSettings> appSettings, IHttpService httpService)
        {
            _httpService = httpService;
            _appSettings = appSettings.Value;
        }
        public async Task<ExecutionResponse<object>> GetPlans(PlanSearchVm model)
        {
            try
            {
                var payload = new
                {
                    Status = model.Status,
                    SavingType = model.SavingType,
                    IsNonInterest = model.IsNonInterest,PageSize = model.PageSize, Page = model.Page
                };
                var serializedRequestContent = JsonConvert.SerializeObject(payload);
                StringContent stringContent = new StringContent(serializedRequestContent, Encoding.UTF8, "application/json");
                var responseResult = await _httpService.Post($"{_appSettings.SavingsServiceUrl}/api/plans", stringContent, new Dictionary<string,string>(), string.Empty);
                var responseContent = await responseResult.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<object>(responseContent);

                if (responseResult.IsSuccessStatusCode)
                {
                    return new ExecutionResponse<object> { Data = result, Message = "Plans retrieved successfully", Status = true };
                }
                else
                {
                    return new ExecutionResponse<object> { Data = result, Message = "Plans was not retrieved successfully", Status = false };
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new ExecutionResponse<object> { Data = null, Message = "Something went wrong.", Status = false };
            }
        }

    }
}   
