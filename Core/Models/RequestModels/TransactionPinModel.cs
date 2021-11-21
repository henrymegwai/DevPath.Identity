using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlinkCash.Core.Models.RequestModels
{
    public class TransactionPinViewModel :Model
    { 
        [Required] public string TransactionPin { get; set; }
        [Required] public AccountType UserType { get; set; }
    }
    public class TransactionPinUpdateViewModel: Model
    { 
        [Required] public string OldTransactionPin { get; set; }
        [Required] public string NewTransactionPin { get; set; }
        [Required] public string ConfirmTransactionPin { get; set; }
        [Required] public AccountType UserType { get; set; }
    }
    public class TransactionPinResetViewModel: Model
    { 
        [Required] public string NewTransactionPin { get; set; }
        [Required] public string ConfirmTransactionPin { get; set; }
        [Required] public AccountType UserType { get; set; }
        [Required] public string Otp { get; set; }
    }
   
    public class TransactionPinCreateViewModel: Model
    { 
        [Required] public string TransactionPin { get; set; }
        [Required] public string ConfirmTransactionPin { get; set; }
        [Required] public AccountType UserType { get; set; }
    }
}
