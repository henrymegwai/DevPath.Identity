using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.AuthModels
{
    public class LoginWithEmailPasswordRequest : Model
    {
        [Required]
        [JsonPropertyName("email")]
        public string UserName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [Required]
        [JsonPropertyName("device")]
        public string Device { get; set; }
    }

    public class LoginWithPhonePasswordRequest : Model
    {
        [Required]
        [JsonPropertyName("phonenumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [Required]
        [JsonPropertyName("device")]
        public string Device { get; set; }
    }

    public class LoginWithEmailRequest : Model
    {
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        
    }

    public class LoginWithPhoneNumberRequest : Model
    {
        [Required]
        [JsonPropertyName("phonenumber")]
        public string PhoneNumber { get; set; }

    }

    public class LoginResult
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }       

        [JsonPropertyName("role")]
        public string[] Role { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        public User User { get; set; }
    }

    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }

    public class ImpersonationRequest
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
