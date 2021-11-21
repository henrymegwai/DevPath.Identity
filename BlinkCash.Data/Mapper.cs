
using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models;
using BlinkCash.Core.Models.AuthModels;
using BlinkCash.Data.Entities;
using System;

namespace BlinkCash.Data
{
    public static class Mapper
    {
        public static User Map(this IdentityUserExtension model)
        {
            if (model == null)
                return null;

            return new User
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                Device = model.Device,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Othernames = model.Othernames,
                PhotoUrl = model.PhotoUrl,
                PreferedName = model.PreferredName, 
                HasSecretQuestion = model.HasSecretQuestion
            };
        }
        
        public static ConfirmationTokenDto Map(this ConfirmationToken entity)
        {
            if (entity == null)
                return null;

            return new ConfirmationTokenDto
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                TokenId = entity.TokenId,
                SSOToken = entity.SSOToken,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate
            };
        }
        public static ConfirmationToken Map(this ConfirmationTokenDto model)
        {
            if (model == null)
                return null;

            return new ConfirmationToken
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                TokenId = model.TokenId,
                SSOToken = model.SSOToken
            };
        }



        public static AccountDto Map(this Account model)
        {
            if (model == null)
                return null;

            return new AccountDto
            {                
                Id = model.Id, AccountId = model.AccountId, AccountType = model.AccountType, HasTransactionPin = model.HasTransactionPin, UpdateFlag= model.UpdateFlag, IsTransactionPinHashed = model.IsTransactionPinHashed, TransactionPin = model.TransactionPin
            };
        }
        public static Account Map(this AccountDto model)
        {
            if (model == null)
                return null;

            return new Account
            {
                Id = model.Id,
                AccountId = model.AccountId,
                AccountType = model.AccountType,UserId = model.UserId
            };
        }

        public static SecurityQuestionDto Map(this SecurityQuestion model)
        {
            if (model == null)
                return null;

            return new SecurityQuestionDto
            {
                Id = model.Id,
                Question = model.Question
                 
            };
        }
        public static SecurityQuestion Map(this SecurityQuestionDto model)
        {
            if (model == null)
                return null;

            return new SecurityQuestion
            {
                Id = model.Id,
                Question = model.Question
            };
        }  
        
        public static UserSecurityQuestionAndAnswerDto Map(this UserSecurityQuestionAndAnswer model)
        {
            if (model == null)
                return null;

            return new UserSecurityQuestionAndAnswerDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Answer = model.Answer,
                RecordStatus = model.RecordStatus,
                SecurityQuestionId = model.SecurityQuestionId,
                SecurityQuestion = model.SecurityQuestion != null ? model.SecurityQuestion.Map() : new SecurityQuestionDto { },
                CreatedDate = model.CreatedDate

            };
        }
        public static UserSecurityQuestionAndAnswer Map(this UserSecurityQuestionAndAnswerDto model)
        {
            if (model == null)
                return null;

            return new UserSecurityQuestionAndAnswer
            {
                Id = model.Id, 
                UserId = model.UserId,
                Answer = model.Answer,
                RecordStatus = model.RecordStatus,
                SecurityQuestionId = model.SecurityQuestionId,  CreatedBy = model.CreatedBy
            };
        }
    }
}
