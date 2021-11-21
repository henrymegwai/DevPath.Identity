using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Models.NubanModels
{
    public class NubanBalanceRequest:Model
    {
        [Required]
        public string Nuban { get; set; }
    }

    
}
