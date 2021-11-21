using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Dtos
{
    public class AccountDto: BaseDto
    {
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public string UserId { get; set; }
        public bool HasTransactionPin { get; set; } = false;
        public string TransactionPin { get; set; }
        public bool IsTransactionPinHashed { get; set; } = false;
        public bool HasRequestedCard { get; set; } = false;
        public bool UpdateFlag { get; set; } = false;
    }
}
