using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models;
using BlinkCash.Core.Models.AuthModels;
using BlinkCash.Core.Models.BvnModels;
using BlinkCash.Core.Models.DomainModels;
using BlinkCash.Core.Models.OtpModels;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface IAuthManager
    {
        Task<ExecutionResponse<AccountDto>> GetAccount();
        bool IsAnExistingUser(string userName);
        Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentials(string userName, string password);
        Task<(string[] role, long UseId)> GetUserRole(IdentityUserExtension signedUser);
        Task<ExecutionResponse<SignUpResponse>> CreateUser(SignUpRequestModel model);
        Task<ExecutionResponse<string>> ForgotPassword(ForgotPasswordRequestModel model);
        Task<ExecutionResponse<CheckPhoneNumberExistResponse>> CheckPhoneNumber(CheckPhoneNumberRequestModel model); 
        Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentialsWithoutPassowrd(string email);
        Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentialsWithPhoneNumber(string phoneNumber, string password);
        Task<ExecutionResponse<User[]>> GetUsers(Role roletype, int skip = 0, int take = 50);
        Task<ExecutionResponse<CreateUserResponse>> CreateBackEndUser(UserRequestModel model, Role roleType);
        Task<ExecutionResponse<UpdatePreferredNameResponse>> UpdatePreferedName(UpdatePreferredNameRequest model);
        Task<ExecutionResponse<string>> ResetPassword(ResetPasswordModel model);
        Task<ExecutionResponse<string>> ChangePassword(ChangePasswordModel model);
        BvnResponse GenerateBVN();
        Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentialsWithPhoneNumber(string phoneNumber);
        Task<ExecutionResponse<VerifyOtpPhoneNumberResponse>> VerifyOtpForgotPassword(VerifyOtpPhoneNumberRequest model);
        Task<ExecutionResponse<string>> UpdateAccountWithHasRequestedCard(string accountId, bool hasRequestedCard);
        Task<(string[] role, long UseId)> GetUserRole(string email);
        Task<ExecutionResponse<UpdateUserResponseModel>> UpdateUser(UpdateUserRequestModel model, string phoneNumber);
        Task<ExecutionResponse<FrequentlyDialedNumberResponseModel>> UpdateFrequentlyDialedNumber(FrequentlyDialedNumberRequestModel model, string phoneNumber);
    }
}
