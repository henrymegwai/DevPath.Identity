using BlinkCash.Core.Configs;
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Models;
using BlinkCash.Core.Models.AuthModels;
using BlinkCash.Core.Models.BvnModels;
using BlinkCash.Core.Models.DomainModels;
using BlinkCash.Core.Models.NubanModels;
using BlinkCash.Core.Models.OtpModels;
using BlinkCash.Core.Models.RequestModels;
using BlinkCash.Core.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly ILogger<AuthManager> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ISecurityQuestionManager _securityQuestionManager;
        private readonly AppSettings _appSettings;
        private readonly IConfirmationTokenRepository _confirmationRepository;
        private readonly SignInManager<IdentityUserExtension> _signInManager;
        private readonly UserManager<IdentityUserExtension> _userManager;
        private readonly IUtilityService _utilityService;
        private readonly INotificationService _emailService;
        private readonly INubanService _nubanCreationService;
        private readonly IOtpService _otpService;
        private readonly IAccountRepository _accountRepository; private readonly IUserRepository _userRepository;

        private readonly IDictionary<string, string> _users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" },
            { "admin", "securePassword" }
        };

        public AuthManager(ILogger<AuthManager> logger, SignInManager<IdentityUserExtension> signInManager,
            UserManager<IdentityUserExtension> userManager, INubanService nubanCreationService, IAccountRepository accountRepository,
            IUtilityService utilityService,
            IUserRepository userRepository,
            INotificationService emailService,
            IConfirmationTokenRepository confirmationRepository,
            IOptions<AppSettings> appSettings,
            IOtpService otpService, 
            ISecurityQuestionManager securityQuestionManager,
             IWebHostEnvironment env)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _nubanCreationService = nubanCreationService;
            _accountRepository = accountRepository;
            _utilityService = utilityService;
            _userRepository = userRepository;
            _emailService = emailService;
            _confirmationRepository = confirmationRepository;
            _appSettings = appSettings.Value;
            _otpService = otpService;
            _securityQuestionManager = securityQuestionManager;
            _env = env;
        }

        public async Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentials(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return (false, null, "Invalid credentials");
            }

            else if (string.IsNullOrWhiteSpace(password))
            {
                return (false, null, "Invalid credentials");
            }

            var signedUser = await _userManager.FindByNameAsync(userName);
            if (signedUser == null)
            {
                return (false, null, "User information could not be verified. Please contact an Administrator.");

            }
            //Get roles of User
            var role = await _userManager.GetRolesAsync(signedUser);
            if (!role.Any())
            {
                return (false, null, "You do not have an assigned role in this system. Please contact an Administrator.");

            }

            var result = await _signInManager.PasswordSignInAsync(userName, password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var account = await _accountRepository.GetAccount(signedUser.Id);
                var user = new User 
                {
                    Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                    Email = signedUser.Email,
                    PhoneNumber = signedUser.PhoneNumber,
                    DateOfBirth = signedUser.DateOfBirth,
                    Device = signedUser.Device,
                    FirstName = signedUser.FirstName,
                    LastName = signedUser.LastName,
                    Othernames = signedUser.Othernames,
                    PhotoUrl = signedUser.PhotoUrl,
                    PreferedName = signedUser.PreferredName,
                    HasSecretQuestion = signedUser.HasSecretQuestion, 
                    HasTransactionPin = account.HasTransactionPin,
                    HasRequestedCard = account.HasRequestedCard,
                    HasPreferredName = signedUser.HasPreferredName,UserName = signedUser.Email, Id = signedUser.Id

                };
                return (true, user, "Login was successful.");

            }
            else
            {
                return (false, null, "User credentials could not be determined. Please cotact Administrator.");
            }
        }


        public async Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentialsWithPhoneNumber(string phoneNumber, string password)
        {
            try
            {
                _logger.LogInformation($"Validating phoneNumber [{phoneNumber}]");
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    return (false, null, "Invalid credentials");
                }

                else if (string.IsNullOrWhiteSpace(password))
                {
                    return (false, null, "Invalid credentials");
                }

                var signedUser = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
                if (signedUser == null)
                {
                    return (false, null, "User information could not be verified. Please contact an Administrator.");

                }
                //Get roles of User
                var role = await _userManager.GetRolesAsync(signedUser);
                if (!role.Any())
                {
                    return (false, null, "You do not have an assigned role in this system. Please contact an Administrator.");

                }

                var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var account = await _accountRepository.GetAccount(signedUser.Id);
                    var user = new User
                    {
                        Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                        Email = signedUser.Email,
                        PhoneNumber = signedUser.PhoneNumber,
                        DateOfBirth = signedUser.DateOfBirth,
                        Device = signedUser.Device,
                        FirstName = signedUser.FirstName,
                        LastName = signedUser.LastName,
                        Othernames = signedUser.Othernames,
                        PhotoUrl = signedUser.PhotoUrl,
                        PreferedName = signedUser.PreferredName,
                        HasSecretQuestion = signedUser.HasSecretQuestion,
                        HasTransactionPin = account.HasTransactionPin,
                        HasRequestedCard = account.HasRequestedCard,
                        HasPreferredName = signedUser.HasPreferredName,
                        UserName = signedUser.Email,
                        Id = signedUser.Id

                    };
                    return (true, user, "Login was successful.");

                }
                else
                {
                    return (false, null, "Invalid credentials");
                }
            }
            catch(Exception ex)
            {
                return (false, null, "Invalid credentials");
            }
           
        }


        public async Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentialsWithoutPassowrd(string email)
        {
            _logger.LogInformation($"Validating user [{email}]");

            var signedUser = await IsEmailValid(email);
            if (signedUser == null)
            {
                return (false, null, "Invalid credentials");
            }

            //Get roles of User
            var role = await _userManager.GetRolesAsync(signedUser);
            if (!role.Any())
            {
                return (false, null, "You do not have an assigned role in this system. Please contact an Administrator.");

            }
            await _signInManager.SignInAsync(signedUser, false, null);

            var account = await _accountRepository.GetAccount(signedUser.Id);
            var user = new User
            {
                Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                Email = signedUser.Email,
                PhoneNumber = signedUser.PhoneNumber,
                DateOfBirth = signedUser.DateOfBirth,
                Device = signedUser.Device,
                FirstName = signedUser.FirstName,
                LastName = signedUser.LastName,
                Othernames = signedUser.Othernames,
                PhotoUrl = signedUser.PhotoUrl,
                PreferedName = signedUser.PreferredName,
                HasSecretQuestion = signedUser.HasSecretQuestion,
                HasTransactionPin = account.HasTransactionPin,
                HasRequestedCard = account.HasRequestedCard,
                HasPreferredName = signedUser.HasPreferredName,
                UserName = signedUser.Email,
                Id = signedUser.Id

            };
            return (true, user, "Login was successful.");

        }


        public async Task<(bool Isvalid, User identityUserExtension, string message)> IsValidUserCredentialsWithPhoneNumber(string phoneNumber)
        {
            _logger.LogInformation($"Validating user [{phoneNumber}]");

            var signedUser = IsPhoneValid(phoneNumber);
            if (signedUser == null)
            {
                return (false, null, "Invalid credentials");
            }

            //Get roles of User
            var role = await _userManager.GetRolesAsync(signedUser);
            if (!role.Any())
            {
                return (false, null, "You do not have an assigned role in this system. Please contact an Administrator.");

            }
            await _signInManager.SignInAsync(signedUser, false, null);
            var account = await _accountRepository.GetAccount(signedUser.Id);
            var user = new User
            {
                Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                Email = signedUser.Email,
                PhoneNumber = signedUser.PhoneNumber,
                DateOfBirth = signedUser.DateOfBirth,
                Device = signedUser.Device,
                FirstName = signedUser.FirstName,
                LastName = signedUser.LastName,
                Othernames = signedUser.Othernames,
                PhotoUrl = signedUser.PhotoUrl,
                PreferedName = signedUser.PreferredName,
                HasSecretQuestion = signedUser.HasSecretQuestion,
                HasTransactionPin = account.HasTransactionPin,
                HasRequestedCard = account.HasRequestedCard,
                HasPreferredName = signedUser.HasPreferredName,
                UserName = signedUser.Email,
                Id = signedUser.Id

            };
            return (true, user, "Login was successful.");

        }


        public bool IsAnExistingUser(string userName)
        {
            return _users.ContainsKey(userName);
        }
        public async Task<IdentityUserExtension> IsEmailValid(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public IdentityUserExtension IsPhoneValid(string phone)
        {
            return _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phone);
        }
        public async Task<(string[] role, long UseId)> GetUserRole(IdentityUserExtension signedUser)
        {
            var roles = await _userManager.GetRolesAsync(signedUser);
            string[] role = roles.ToArray();
            long userId = 0;

            return (role, userId);
        }

        public async Task<(string[] role, long UseId)> GetUserRole(string email)
        {
            var signedUser = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(signedUser);
            string[] role = roles.ToArray();
            long userId = 0;

            return (role, userId);
        }


        public async Task<ExecutionResponse<string>> UpdateAccountWithHasRequestedCard(string accountId, bool hasRequestedCard)
        {
            try
            {
                var account = await _accountRepository.GetAccountById(accountId);
                if (account == null)
                    return new ExecutionResponse<string> { Data = "false", Status = false, Message = "Account does not exist for user" };

                var updatedBy = _utilityService.UserName();
                var result = await _accountRepository.UpdateAccountWithHasRequestedCard(accountId, hasRequestedCard, updatedBy);
                if (result == null)
                    return new ExecutionResponse<string> { Data = "false", Status = false, Message = "Account does not exist for user" };

                return new ExecutionResponse<string> { Data = "true", Message = "Has request card flag was updated for user", Status = true };

            }
            catch (Exception ex)
            {
                //Log here
                return new ExecutionResponse<string> { Data = "", Status = false, Message = "Has request card flag was not updated" };
            }

        }

        public async Task<ExecutionResponse<UpdateUserResponseModel>> UpdateUser(UpdateUserRequestModel model, string phoneNumber)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
                if (user == null)
                    return new ExecutionResponse<UpdateUserResponseModel> { Data = null, Status = false, Message = "phone number does not exist." };
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Othernames = model.Othernames;
                user.Address = model.Address;
                user.DateOfBirth = model.DateOfBirth;
                user.PlaceOfBirth = model.PlaceOfBirth;
                user.Device = model.Device;
                user.PhotoUrl = model.PhotoUrl;
                
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return new ExecutionResponse<UpdateUserResponseModel> { Data = null, Status = false, Message = "Update User profile was not successful." };

                var account = await _accountRepository.GetAccount(user.Id);
                var signUpResponse = new UpdateUserResponseModel
                {
                    Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Othernames = user.Othernames,
                    PhoneNumber = user.PhoneNumber,
                    PhotoUrl = user.PhotoUrl,
                   
                };
                return new ExecutionResponse<UpdateUserResponseModel> { Data = signUpResponse, Message = "Update User profile was successful", Status = true };

            }
            catch (Exception ex)
            {
                //Log here
                return new ExecutionResponse<UpdateUserResponseModel> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }

        }
        public async Task<ExecutionResponse<FrequentlyDialedNumberResponseModel>> UpdateFrequentlyDialedNumber(FrequentlyDialedNumberRequestModel model, string phoneNumber)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
                if (user == null)
                    return new ExecutionResponse<FrequentlyDialedNumberResponseModel> { Data = null, Status = false, Message = "phone number does not exist." };
                user.FrequentlyDialedNumbers = model.Numbers;
                
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return new ExecutionResponse<FrequentlyDialedNumberResponseModel> { Data = null, Status = false, Message = "Update Frequently Dialed Number was not successful." };

                
                var signUpResponse = new FrequentlyDialedNumberResponseModel
                {
                    Numbers = user.FrequentlyDialedNumbers
                };
                return new ExecutionResponse<FrequentlyDialedNumberResponseModel> { Data = signUpResponse, Message = "Update Frequently Dialed Number  was successful", Status = true };

            }
            catch (Exception ex)
            {
                //Log here
                return new ExecutionResponse<FrequentlyDialedNumberResponseModel> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }

        }
        public async Task<ExecutionResponse<UpdatePreferredNameResponse>> UpdatePreferedName(UpdatePreferredNameRequest model)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                if (user == null)
                    return new ExecutionResponse<UpdatePreferredNameResponse> { Data = null, Status = false, Message = "phone number does not exist." };
                user.PreferredName = model.PreferredName;
                user.HasPreferredName = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return new ExecutionResponse<UpdatePreferredNameResponse> { Data = null, Status = false, Message = "Update was not successful." };

                var account = await _accountRepository.GetAccount(user.Id);
                var signUpResponse = new UpdatePreferredNameResponse
                {
                    Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Othernames = user.Othernames,
                    PhoneNumber = user.PhoneNumber,
                    PhotoUrl = user.PhotoUrl,
                    HasSecretQuestion = user.HasSecretQuestion,
                    PreferredName = user.PreferredName,
                    HasPreferredName = user.HasPreferredName,
                    HasTransactionPin = account.HasTransactionPin,  
                };
                return new ExecutionResponse<UpdatePreferredNameResponse> { Data = signUpResponse, Message = "Sign up was successful", Status = true };

            }
            catch (Exception ex)
            {
                //Log here
                return new ExecutionResponse<UpdatePreferredNameResponse> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }

        }

        public async Task<ExecutionResponse<SignUpResponse>> CreateUser(SignUpRequestModel model)
        {
            var userExist = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
            if (userExist != null)
                return new ExecutionResponse<SignUpResponse> { Data = null, Message = "Account with the provide phone number already exist", Status = false };
            string errors = string.Empty;
            try
            {
                NubanCreationRequest nubanCreationRequest = new NubanCreationRequest
                {
                    DateOfBirth = model.DateOfBirth,
                    EmailAddress = model.Email,
                    LastName = model.LastName,
                    OtherNamnes = $"{model.FirstName} {model.Othernames}",
                    PlaceOfBirth = model.PlaceOfBirth,
                    Bvn = model.BVN,
                    Gender = ((int)model.Gender),
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                //Generate Nuban
                var account = await _nubanCreationService.CreateNuban(nubanCreationRequest);
                if (!account.Status)
                    return new ExecutionResponse<SignUpResponse>() { Data = null, Message = $"{account.Message}", Status = account.Status };

                var user = new IdentityUserExtension
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BVN = model.BVN,
                    Device = model.Device,
                    DateOfBirth = model.DateOfBirth,
                    PhoneNumber = model.PhoneNumber,
                    PhotoUrl = model.PhotoUrl,
                    Address = model.Address,
                    Othernames = model.Othernames, PlaceOfBirth = model.PlaceOfBirth
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string currentRole = "Customer";
                    var userRole = await _userManager.AddToRoleAsync(user, currentRole);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    //Create Save Nuban to Account here 
                    AccountDto accountDto = new AccountDto
                    {
                        AccountType = Utilities.AccountType.Customer,
                        AccountId = account.Data.data.accountNumber,
                        UserId = user.Id
                    };
                    var aaccoutCreate = await _accountRepository.CreateAccount(accountDto);

                    //Send email here 
                    //send email here created user

                    var filecontent = System.IO.File.ReadAllText("EmailTemplates/AccountCreation.html");
                    filecontent = filecontent.Replace("{{name}}", user.FirstName + " " + user.LastName);
                    var sendEmail = _emailService.SendEmail(new[] { user.Email }, NotificationConstants.Account_Backend_User, filecontent, true, Array.Empty<string>(), Array.Empty<string>(), string.Empty, Array.Empty<string>());
                    //Create Sign Up
                    var signUpResponse = new SignUpResponse
                    {
                        Account = new AccountModel { AccountId = aaccoutCreate.AccountId, AccountType = aaccoutCreate.AccountType },
                        FirstName = model.FirstName,
                        Email = model.Email,
                        Username = model.Email,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Othernames = model.Othernames,
                        PhoneNumber = model.PhoneNumber,
                        PhotoUrl = model.PhotoUrl,
                        HasSecretQuestion = user.HasSecretQuestion,
                        HasTransactionPin = aaccoutCreate.HasTransactionPin,
                        HasPreferredName = user.HasPreferredName,
                        PreferredName = user.PreferredName,PlaceOfBirth = model.PlaceOfBirth
                    };
                    return new ExecutionResponse<SignUpResponse> { Data = signUpResponse, Message = "Sign up was successful", Status = true };
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        errors += $"{errors}; {error.Description}";
                    }
                    return new ExecutionResponse<SignUpResponse> { Data = null, Message = $"Reason: {errors}", Status = false };
                }

            }
            catch (Exception ex)
            {
                //Log here
                return new ExecutionResponse<SignUpResponse> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }

        }


        public async Task<ExecutionResponse<CreateUserResponse>> CreateBackEndUser(UserRequestModel model, Role roleType)
        {

            string errors = string.Empty;
            try
            {
                var user = new IdentityUserExtension
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    PhotoUrl = model.PhotoUrl,
                    Othernames = model.Othernames
                };
                var password = Extensions.PasswordGenerateCode(model.Email);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    string currentRole = roleType.ToString();
                    var userRole = await _userManager.AddToRoleAsync(user, currentRole);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var cToken = await CreateToken(token);

                    //send email here created user
                    var baseUrl = _appSettings.BlinkCashWebUrl;
                    string comfirmationLink = $"{baseUrl}/#/confirm_email/?userId={user.Id}&token={cToken.TokenId}&email={user.Email}";
                    var filecontent = System.IO.File.ReadAllText("EmailTemplates/UserCreation.html");
                    filecontent = filecontent.Replace("{{name}}", user.FirstName + " " + user.LastName);
                    filecontent = filecontent.Replace("{{activateaccounturl}}", comfirmationLink);
                    var sendEmail = _emailService.SendEmail(new[] { user.Email }, NotificationConstants.Account_Backend_User, filecontent, true, Array.Empty<string>(), Array.Empty<string>(), string.Empty, Array.Empty<string>());

                    //Create Sign Up
                    var signUpResponse = new CreateUserResponse
                    {

                        FirstName = model.FirstName,
                        Email = model.Email,
                        Username = model.Email,
                        LastName = model.LastName,
                        Othernames = model.Othernames,
                        PhoneNumber = model.PhoneNumber,
                        PhotoUrl = model.PhotoUrl,
                        Active = user.Active,
                        HasResetPassword = user.HasResetPasswordForAdmin
                    };
                    return new ExecutionResponse<CreateUserResponse> { Data = signUpResponse, Message = "User was successful created", Status = true };
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        errors += $"{errors}; {error.Description}";
                    }
                    return new ExecutionResponse<CreateUserResponse> { Data = null, Message = $"Reason: {errors}", Status = false };
                }

            }
            catch (Exception ex)
            {
                //Log here
                return new ExecutionResponse<CreateUserResponse> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }

        }


        public async Task<ExecutionResponse<string>> ForgotPassword(ForgotPasswordRequestModel model)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                if (user == null)
                    return new ExecutionResponse<string> { Data = null, Message = "Phone number does not exist", Status = false };

                //send otp to phonenumber here

                var otp = await _otpService.Send(new OtpRequest { PhoneNumber = model.PhoneNumber });
                if (otp.Status == true)
                    return new ExecutionResponse<string> { Data = $"Request was successful. Otp sent: {otp.Data.Data}", Status = true, Message = "Otp has been sent to your phone" };

                return new ExecutionResponse<string> { Data = "", Status = false, Message = "Cannot initiate request at this time. Please try again." };

            }
            catch (Exception ex)
            {
                return new ExecutionResponse<string> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }
        }


        public async Task<ExecutionResponse<VerifyOtpPhoneNumberResponse>> VerifyOtpForgotPassword(VerifyOtpPhoneNumberRequest model)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                if (user == null)
                    return new ExecutionResponse<VerifyOtpPhoneNumberResponse> { Data = null, Message = "Phone number does not exist", Status = false };

                //send otp to phonenumber here
                var otp = await _otpService.Verify(new OtpRequest { PhoneNumber = model.PhoneNumber });
                if (otp.Status == true)
                    return new ExecutionResponse<VerifyOtpPhoneNumberResponse> { Data = new VerifyOtpPhoneNumberResponse { Otp = otp.Data.Data.ToString() }, Status = true, Message = "Otp has been sent to your phone" };

                return new ExecutionResponse<VerifyOtpPhoneNumberResponse> { Data = null, Status = false, Message = "Cannot initiate request at this time. Please try again." };

            }
            catch (Exception ex)
            {
                return new ExecutionResponse<VerifyOtpPhoneNumberResponse> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }
        }

        public async Task<ExecutionResponse<string>> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                if (user == null)
                    return new ExecutionResponse<string> { Data = null, Message = "Invalid phone number", Status = false };

                //validate security question and answer
                var IsAnswerValid = await _securityQuestionManager.VerifyUserAnswer(new VerifyAnswerRequest { Answer = model.SecurityAnswer, PhoneNumber = model.PhoneNumber, SecurityQuestionId = model.SecurityQuestionId });
                if (IsAnswerValid.Status && !IsAnswerValid.Data.DoesAnswerMatch)
                    return new ExecutionResponse<string> { Data = null, Message = "Answer to Security Question does not match", Status = false };

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPassword = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);
                if (!resetPassword.Succeeded)
                    return new ExecutionResponse<string> { Data = null, Message = "Unable to reset password at this time please contact Administrator", Status = false };

                return new ExecutionResponse<string> { Data = user.PhoneNumber, Message = "Password Reset was successful", Status = true };

            }
            catch (Exception ex)
            {
                return new ExecutionResponse<string> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }
        }


        public async Task<ExecutionResponse<string>> ChangePassword(ChangePasswordModel model)
        {

            try
            {
                var userName = _utilityService.UserName();
                var entity = await _userManager.FindByNameAsync(userName);
                if (entity == null)
                {
                    // Don't reveal that the user does not exist
                    return new ExecutionResponse<string>() { Data = "", Message = $"Change Password Reuqest failed, user information was not found", Status = false };
                }
                var result = await _userManager.ChangePasswordAsync(entity, model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    string error = string.Empty;
                    var errors = result.Errors.Select(c => c.Description).ToList();
                    foreach (var eror in errors)
                    {
                        error += $"{eror} ";
                    }
                    return new ExecutionResponse<string>() { Data = "", Message = $"Change Password Reuqest failed, {error}", Status = false };

                }

                return new ExecutionResponse<string>() { Data = "", Message = $"Change Password Request was successful", Status = true };

            }
            catch (Exception ex)
            {
                return new ExecutionResponse<string>() { Data = "", Message = $"Change Password Reuqest failed, {ex.Message}", Status = false };

            }


        }



        public async Task<ExecutionResponse<CheckPhoneNumberExistResponse>> CheckPhoneNumber(CheckPhoneNumberRequestModel model)
        {
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                if (user != null)
                {
                    var account = await _accountRepository.GetAccount(user.Id);
                    var phoneExitResponse = new CheckPhoneNumberExistResponse
                    {
                        Account = new AccountModel { AccountId = account.AccountId, AccountType = account.AccountType },
                        Device = model.Device,
                        PhoneNumberExist = true,
                        Active = user.Active,
                        FirstName = user.FirstName,
                        HasPreferredName = user.HasPreferredName,
                        HasTransactionPin = account.HasTransactionPin,
                        HasSecretQuestion = user.HasSecretQuestion,
                        HasRequestedCard = account.HasRequestedCard,
                        LastName = user.LastName,
                        Othernames = user.Othernames,
                        PhoneNumber = user.PhoneNumber,
                        PreferredName = user.PreferredName,  PhotoUrl = user.PhotoUrl
                    };
                    return new ExecutionResponse<CheckPhoneNumberExistResponse> { Data = phoneExitResponse, Message = "phone number  exist", Status = true };

                }
                var phoneExistResponse = new CheckPhoneNumberExistResponse { PhoneNumberExist = false, PhoneNumber = model.PhoneNumber, Device = model.Device };
                return new ExecutionResponse<CheckPhoneNumberExistResponse> { Data = phoneExistResponse, Status = false, Message = "phone number does not exist." };

            }
            catch (Exception ex)
            {
                return new ExecutionResponse<CheckPhoneNumberExistResponse> { Data = null, Message = $"Reason: {ex.Message}", Status = false };
            }
        }

        public BvnResponse GenerateBVN()
        {
            string first = new Random().Next(468300, 804884).ToString();
            string second = new Random().Next(16830, 33488).ToString();
            string phone = new Random().Next(1, 9).ToString();

            string path = Path.Combine($"{_env.ContentRootPath}//Image//pexels-photo-1841819.jpeg");
            byte[] b = System.IO.File.ReadAllBytes(path);

            var bvnresponse = new BvnResponse
            {
                BVN = $"{second}{first}",
                FirstName = $"Julian-{first}",
                LastName = $"Oti-{second}",
                DateOfBirth = $"1995-09-30T10:49:31.770Z",
                Email = $"Julian-{first}@yopmail.com".ToLower(),
                Gender = "Male",
                MiddleName = $"Obi{second}",
                PhoneNumber1 = $"080{phone}5{first}",
                ResidentialAddress = $"House {phone}, Uptown Street, Lord Lugard Rd.,Ikoyi",
                Base64Image = "data:image/png;base64," + Convert.ToBase64String(b)
            };
       
           
            return  bvnresponse;
        }

        public async Task<ExecutionResponse<AccountDto>> GetAccount()
        {
            string userId = _utilityService.UserId();
            if (string.IsNullOrEmpty(userId))
                return new ExecutionResponse<AccountDto> { Data = null, Message = $"Unauthorized Request", Status = false };

            var aaccout = await _accountRepository.GetAccount(userId);
            var newAccount = new AccountDto { AccountId = aaccout.AccountId };
            return new ExecutionResponse<AccountDto> { Data = newAccount, Message = $"Account successfully retrieved", Status = true };
        }


        public async Task<ExecutionResponse<User[]>> GetUsers(Role roletype, int skip = 0, int take = 50)
        {
            new ExecutionResponse<User[]>();

            try
            {
                var users = await _userRepository.GetUserByRole(roletype, skip, take);
                var response = new ExecutionResponse<User[]> { Data = users, Message = $"{roletype.ToString()} users successfully retrieved", Status = true };
                return response;
            }
            catch (Exception ex)
            {
                var response = new ExecutionResponse<User[]> { Data = null, Message = $"No record found.", Status = false };
                return response;
            }


        }
        public async Task<ConfirmationTokenDto> CreateToken(string token)
        {
            var model = new ConfirmationTokenDto
            {
                TokenId = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower(),
                SSOToken = token
            };

            return await _confirmationRepository.Create(model);
        }
    }
}
