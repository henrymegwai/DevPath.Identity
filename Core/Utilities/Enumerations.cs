using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Utilities
{
    public enum RecordStatus
    {
        Active = 1,
        Inactive,
        Deleted,
        Archive
    }
    
    public enum AccountType
    {
        Customer = 1,
        Agent,
        
    }
    public enum PlanStatus
    {
        Active, NotActive, Onhold
    }

    public enum PlanStatusCategory
    {
       All, Active, NotActive, Onhold
    }

    public enum SavingType
    {
        Fixed = 1,
        Target, Regular

    }
    public enum Role
    {
        
        Customer,
        Agent,
        [Description("Administrator")]
        Administrator,
        [Description("SuperAdmin")]
        SuperAdmin,
        [Description("User")]
        User

    }
    public enum Gender
    {
        Female = 0,
        Male = 1
      
        
    }
}
