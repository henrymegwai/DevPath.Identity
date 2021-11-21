using BlinkCash.Core.Configs;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models.NubanModels;
using BlinkCash.Core.Models.OtpModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class OtpService : IOtpService
    {
        private readonly IHttpService _httpService;
        private readonly AppSettings _appSettings;
        public OtpService(IOptions<AppSettings> appSettings, IHttpService httpService)
        {
            _httpService = httpService;
            _appSettings = appSettings.Value;
        }
        public async Task<ExecutionResponse<OtpResponse>> Send(OtpRequest model)
        {
            try
            {
                var payload = new
                { 
                    phoneNumber = model.PhoneNumber, 
                };
                var serializedRequestContent = JsonConvert.SerializeObject(payload);
                StringContent stringContent = new StringContent(serializedRequestContent, Encoding.UTF8, "application/json");
                var headers = new Dictionary<string, string>{}; 
                var responseResult = await _httpService.Post($"{_appSettings.OtpUrl}/api/otp/send", stringContent, headers, string.Empty);
                var responseContent = await responseResult.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OtpResponse>(responseContent);

                if (responseResult.IsSuccessStatusCode)
                {
                    return new ExecutionResponse<OtpResponse> { Data = result, Message = result.Message, Status = true };
                }
                else
                {
                    return new ExecutionResponse<OtpResponse> { Data = result, Message = result.Message, Status = result.Status };
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new ExecutionResponse<OtpResponse> { Data = null, Message = ex.Message, Status = false };
            }
        }

        public async Task<ExecutionResponse<OtpResponse>> Verify(OtpRequest model)
        {
            try
            {
                var payload = new
                {
                    phoneNumber = model.PhoneNumber,
                };
                var serializedRequestContent = JsonConvert.SerializeObject(payload);
                StringContent stringContent = new StringContent(serializedRequestContent, Encoding.UTF8, "application/json");
                var headers = new Dictionary<string, string> { };
                var responseResult = await _httpService.Post($"{_appSettings.OtpUrl}api/otp/send", stringContent, headers, string.Empty);
                var responseContent = await responseResult.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OtpResponse>(responseContent);

                if (responseResult.IsSuccessStatusCode)
                {
                    return new ExecutionResponse<OtpResponse> { Data = result, Message = result.Message, Status = true };
                }
                else
                {
                    return new ExecutionResponse<OtpResponse> { Data = result, Message = result.Message, Status = result.Status };
                }

            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new ExecutionResponse<OtpResponse> { Data = null, Message = "Something went wrong, pleae try again", Status = false };
            }
        }

        
    }
}
