using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.AuthModels
{

    public class UpdatePreferredNameRequest : Model
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PreferredName { get; set; }

    }

   
    public class UpdatePreferredNameResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BVN { get; set; }
        public string PhotoUrl { get; set; }
        public bool HasTransactionPin { get; set; }
        public bool HasSecretQuestion { get; set; }
        public bool HasPreferredName { get;  set; }
        public string PreferredName { get;  set; }
        public AccountModel Account { get;  set; }
    }
    public class SignUpRequestModel : Model
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Othernames { get; set; }
        [Required]  
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [StringLength(11, ErrorMessage = "BVN charater length must be 11 characters ", MinimumLength = 11)]
        public string BVN { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        public string Device { get; set; }
        public Gender Gender { get;  set; } = Gender.Male;
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
    }

    public class UpdateUserRequestModel : Model
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Othernames { get; set; } 
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string PhotoUrl { get; set; }
        public string Device { get; set; }
        public Gender Gender { get; set; } = Gender.Male;
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
    }

    public class FrequentlyDialedNumberRequestModel : Model
    {
        [Required]
        public string Numbers { get; set; }
        
    }
    public class FrequentlyDialedNumberResponseModel 
    {
        [Required]
        public string Numbers { get; set; }

    }
    public class UpdateUserResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public AccountModel Account { get; set; }
    }

    public class SignUpResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BVN { get; set; }
        public string PhotoUrl { get; set; }
        public bool HasTransactionPin { get; set; }
        public bool HasSecretQuestion { get; set; }
        public AccountModel Account { get; set; }
        public string Username { get;   set; }
        public bool HasPreferredName { get;   set; }
        public string PreferredName { get;   set; }
    }

    public class CreateUserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public bool HasResetPassword { get; set; }
        public bool Active { get; set; }
        public string Username { get;  set; }
    }


    public class AccountModel
    {
        public long Id { get; set; }
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class ForgotPasswordRequestModel : Model
    {
        public string PhoneNumber { get; set; }

    }
    public class ResetPasswordModel : Model
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public long SecurityQuestionId { get; set; }
        [Required]
        public string SecurityAnswer { get; set; }
        [Required]
        public string Otp { get; set; } 
    }

    public class ChangePasswordModel:Model
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

    }

    public class CheckPhoneNumberRequestModel : Model
    {
        public string PhoneNumber { get; set; }
        public string Device { get; set; }

    }


    public class UserRequestModel : Model
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Othernames { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
       
        public string PhotoUrl { get; set; }
        [Required]
        public Role Role { get; set; }
        public Gender Gender { get;  set; } = Gender.Male;
    }

}
