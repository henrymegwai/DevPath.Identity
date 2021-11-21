using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models.RequestModels;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class PinService : IPinService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUtilityService _utilityService;
        public PinService(IAccountRepository accountRepository, IUtilityService utilityService)
        {
            _accountRepository = accountRepository;
            _utilityService = utilityService;
        }

        public  async Task<ExecutionResponse<string>> ChangePin(TransactionPinUpdateViewModel model )
        {
            var hashedOldPin = _utilityService.ComputeHash(model.OldTransactionPin);
            var hashedNewPin = _utilityService.ComputeHash(model.NewTransactionPin);
            var response = new ExecutionResponse<string> { };
            try
            {

                var userId = _utilityService.UserId();
               
                switch (model.UserType)
                {
                    case AccountType.Customer:
                        var customer = await _accountRepository.GetAccount(userId);
                        if (customer == null) 
                            return new ExecutionResponse<string> { Message = "Invalid account!", Data = "", Status = false };  
                        if (!customer.HasTransactionPin) 
                            return new ExecutionResponse<string> { Message = "No transaction PIN setup for user", Data = "", Status=false };

                        if (!customer.IsTransactionPinHashed)
                        {
                            var hashedPin = _utilityService.ComputeHash(customer.TransactionPin);
                            customer.TransactionPin = hashedPin;
                            customer.IsTransactionPinHashed = true;
                            await _accountRepository.UpdateAccount(customer, customer.Id);
                        }
                        CompareConfirmTransactionPin(model.ConfirmTransactionPin, model.NewTransactionPin); 
                        if (!hashedOldPin.Equals(customer.TransactionPin)) 
                            return new ExecutionResponse<string> { Message = "Invalid PIN", Data = "", Status = false };  
                        customer.TransactionPin = hashedNewPin;
                        customer.HasTransactionPin = true;
                        customer.UpdateFlag = true;
                        customer.IsTransactionPinHashed = true;
                        await _accountRepository.UpdateAccount(customer, customer.Id);
                        return new ExecutionResponse<string> { Message = "Change of Transaction Pin was Successful", Data = "", Status = true };

                    case AccountType.Agent:
                        var agent = await _accountRepository.GetAccount(userId);
                        if (agent == null) return new ExecutionResponse<string> { Message = "Invalid account!", Data = "", Status = false }; 
                        if (agent.HasTransactionPin) return new ExecutionResponse<string> { Message = "Transaction PIN already created for account", Data = "", Status = false };
                        CompareConfirmTransactionPin(model.ConfirmTransactionPin, model.NewTransactionPin);
                        agent.TransactionPin = hashedNewPin;
                        agent.HasTransactionPin = true;
                        agent.UpdateFlag = true;
                        agent.IsTransactionPinHashed = true;
                        await _accountRepository.UpdateAccount(agent, agent.Id);
                        return response = new ExecutionResponse<string> { Data = "", Message = "Successful Created Transaction Pin", Status = true };

                    default:
                        throw new Exception("Invalid User Type");
                }
            }
            catch (Exception ex)
            {

                new Exception(ex.Message);
                return new ExecutionResponse<string> { Data = null, Message = "Something went wrong.", Status = false };
            }

        }

        public async Task<ExecutionResponse<string>> CreatePin(TransactionPinCreateViewModel model)
        {
            var response = new ExecutionResponse<string> { };
            try
            {

                var userId = _utilityService.UserId();
                var hashedNewPin = _utilityService.ComputeHash(model.TransactionPin);
                switch (model.UserType)
                {
                    case AccountType.Customer:
                        var customer = await _accountRepository.GetAccount(userId);
                        if (customer == null) return new ExecutionResponse<string> { Data = "", Message = "Invalid Account!", Status = false };
                        if (customer.HasTransactionPin) return new ExecutionResponse<string> { Data = "", Message = "Transaction PIN already created for this account", Status = false }; 
                        CompareConfirmTransactionPin(model.ConfirmTransactionPin, model.TransactionPin);
                        customer.TransactionPin = hashedNewPin;
                        customer.HasTransactionPin = true;
                        customer.UpdateFlag = true; 
                        customer.IsTransactionPinHashed = true;
                        await  _accountRepository.UpdateAccount(customer, customer.Id);
                        return response =  new ExecutionResponse<string> { Data = "", Message = "Successful Created Transaction Pin", Status = true };
                   
                      
                    case AccountType.Agent:
                        var agent = await _accountRepository.GetAccount(userId);
                        if (agent == null) return new ExecutionResponse<string> { Data = "", Message = "Invalid Account!", Status = false };
                        if (agent.HasTransactionPin) return new ExecutionResponse<string> { Data = "", Message = "Transaction PIN already created for this account", Status = false };
                        CompareConfirmTransactionPin(model.ConfirmTransactionPin, model.TransactionPin);
                        agent.TransactionPin = hashedNewPin;
                        agent.HasTransactionPin = true;
                        agent.UpdateFlag = true; 
                        agent.IsTransactionPinHashed = true;
                        await _accountRepository.UpdateAccount(agent, agent.Id);
                        return response = new ExecutionResponse<string> { Data = "", Message = "Successful Created Transaction Pin", Status = true };

                    default:
                        return new ExecutionResponse<string> { Data = "", Message = "Invalid User Type", Status = false }; 
                }
            }
            catch (Exception ex)
            {
                 
                new Exception(ex.Message);
                return new ExecutionResponse<string> { Data = null, Message = "Something went wrong.", Status = false };
            }
           
        }

        public Task<ExecutionResponse<string>> ResetPin(TransactionPinResetViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ExecutionResponse<PinVerificationResponse>> VerifyPin(TransactionPinViewModel model)
        {
            throw new NotImplementedException();
        }


        #region Private Methods
        private async Task<bool> CompareHashedStrings(string sourcePin, string hashedPin)
        {
            var comparedHash = _utilityService.ComputeHash(sourcePin);
            if (!hashedPin.Equals(comparedHash)) throw new Exception("Invalid Transaction PIN");

            return await Task.FromResult(true);
        }
        public void CompareConfirmTransactionPin(string confirmPin, string newPin)
        {
            if (!confirmPin.Equals(newPin)) throw new Exception("Pin and confirm pin don't match");
        }
         
        #endregion
    }
}
