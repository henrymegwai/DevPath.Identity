using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models
{
    public class PlanSearchVm
    {
        public PlanStatusCategory Status { get; set; }
        public SavingType SavingType { get; set; }
        public bool IsNonInterest { get; set; }
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 1;
    }
}
