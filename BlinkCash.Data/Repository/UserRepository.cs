using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Models;
using BlinkCash.Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUserExtension> _userManager;
        
        public UserRepository(UserManager<IdentityUserExtension> userManager)
        {
            _userManager = userManager;
        }
                
        public async Task<User[]> GetUserByRole(Role roleName, int skip, int take)
        {
            skip = skip * take;
            var cutomers = await _userManager.GetUsersInRoleAsync(roleName.ToString());
            var entities = cutomers.Skip(skip).Take(take).Select(x => x.Map()).ToArray();
            if (entities.Length == 0)
                return Array.Empty<User>();
            return entities;
        }
    }
}
