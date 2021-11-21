using BlinkCash.Core.Dtos;
using BlinkCash.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User[]> GetUserByRole(Role roleName, int skip, int take);
    }
}
