using BlinkCash.Core.Configs;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models.NubanModels;
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
    public class NubanService : INubanService
    {
        private readonly IHttpService _httpService;
        private readonly AppSettings _appSettings;
        public NubanService(IOptions<AppSettings> appSettings, IHttpService httpService)
        {
            _httpService = httpService;
            _appSettings = appSettings.Value;
        }
        public async Task<ExecutionResponse<NubanCreationResponse>> CreateNuban(NubanCreationRequest model)
        {
            try
            {
                var payload = new
                {
                    lastName = model.LastName,
                    otherNames = model.OtherNamnes,
                    bvn = model.Bvn,
                    phoneNumber = model.PhoneNumber,
                    gender = model.Gender,
                    placeOfBirth = string.IsNullOrEmpty(model.PlaceOfBirth) ? "" : model.PlaceOfBirth,
                    dateOfBirth = model.DateOfBirth,
                    address = model.Address,
                    emailAddress = model.EmailAddress
                };
                var serializedRequestContent = JsonConvert.SerializeObject(payload);
                StringContent stringContent = new StringContent(serializedRequestContent, Encoding.UTF8, "application/json");
                var headers = new Dictionary<string, string>{};
                headers.Add("ACCESS-KEY", _appSettings.NubanAccessKey);
                var responseResult = await _httpService.Post($"{_appSettings.NubanServiceUrl}/customers", stringContent, headers, string.Empty);
                var responseContent = await responseResult.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NubanCreationResponse>(responseContent);

                if (responseResult.IsSuccessStatusCode)
                {
                    return new ExecutionResponse<NubanCreationResponse> { Data = result, Message = result.message, Status = true };
                }
                else
                {
                    return new ExecutionResponse<NubanCreationResponse> { Data = result, Message = result.message, Status = result.error };
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new ExecutionResponse<NubanCreationResponse> { Data = null, Message = ex.Message, Status = false };
            }
        }

        public async Task<ExecutionResponse<NubanBalanceResponse>> NubanBalance(NubanBalanceRequest model)
        {
            try
            {
                var headers = new Dictionary<string, string> { };
                headers.Add("ACCESS-KEY", _appSettings.NubanAccessKey);
                var responseResult = await _httpService.Get($"{_appSettings.NubanServiceUrl}/cutomers/{model.Nuban}/balance", headers, string.Empty);
                var responseContent = await responseResult.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NubanBalanceResponse>(responseContent);

                return new ExecutionResponse<NubanBalanceResponse> { Data = result, Message = result.Message, Status = result.Error };


            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new ExecutionResponse<NubanBalanceResponse> { Data = null, Message = "Something went wrong, pleae try again", Status = false };
            }
        }

        public async Task<ExecutionResponse<WithdrawalResponse>> InterBankTransfer(WithdrawalRequest model)
        {
            try
            {
                var payload = new
                {
                    amount = model.Amount,
                    payer = model.Payer,
                    fromAccountNumber = model.FromAccountNumber,
                    toAccountNumber = model.ToAccountNumber,
                    receiverAccountType = model.ReceiverAccountType,
                    receiverBankCode = model.ReceiverBankCode,
                    receiverPhoneNumber = model.ReceiverPhoneNumber,
                    narration = model.Narration
                };
                var serializedRequestContent = JsonConvert.SerializeObject(payload);
                StringContent stringContent = new StringContent(serializedRequestContent, Encoding.UTF8, "application/json");
                var headers = new Dictionary<string, string> { };
                headers.Add("ACCESS-KEY", _appSettings.NubanAccessKey);
                var responseResult = await _httpService.Post($"{_appSettings.NubanServiceUrl}/transactions/interBank", stringContent,headers , string.Empty);
                var responseContent = await responseResult.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<WithdrawalResponse>(responseContent);

                if (responseResult.IsSuccessStatusCode)
                {
                    return new ExecutionResponse<WithdrawalResponse> { Data = result, Message = "", Status = result.error };
                }
                else
                {
                    return new ExecutionResponse<WithdrawalResponse> { Data = result, Message = "", Status = result.error };
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return new ExecutionResponse<WithdrawalResponse> { Data = null, Message = "Something went wrong.", Status = false };
            }
        }
    }
}
