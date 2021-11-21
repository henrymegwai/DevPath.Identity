using BlinkCash.Core.Models.JwtModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Managers
{
    public interface IJwtManager
    {
        /// <summary>
        /// 
        /// </summary>
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        /// <summary>
        /// 
        /// </summary>
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
        /// <summary>
        /// 
        /// </summary>
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
        /// <summary>
        /// 
        /// </summary>
        void RemoveExpiredRefreshTokens(DateTime now);
        /// <summary>
        /// 
        /// </summary>
        void RemoveRefreshTokenByUserName(string userName);
        /// <summary>
        /// 
        /// </summary>
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}
