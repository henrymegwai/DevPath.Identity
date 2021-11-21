using BlinkCash.Core.Dtos;
using BlinkCash.Core.Models.JwtModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.DIExtensions
{
    public static class SecurityConfiguration
    {
        public static void AddSecurityServices(this IServiceCollection services,
            IConfiguration Configuration)
        {

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                 
                opts.RequireHttpsMetadata = false;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateAudience = true,
                    //ValidAudiences = new[]
                    //{
                    //        $"{opts.Authority}/resources",
                    //        "ebipsgatewayapi"
                    //    }
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)

                };
                //opts.Events = new JwtBearerEvents
                //{
                //    OnAuthenticationFailed = async context =>
                //    {
                //        string s = context.HttpContext.Request.Path;

                //        if (!s.ToLower().Contains("/api/auth"))
                //        {
                //            Exception ex = context.Exception;

                //            var result = new ExecutionResponse<string>()
                //            {
                //                Data = string.Empty, Status = false,   
                //                Message = HttpStatusCode.Unauthorized.ToString()
                //            };
                //            try
                //            {

                //                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //                context.Response.ContentType = "application/json";
                //            }
                //            catch (Exception exx)
                //            {
                //               // Log.Info(exx.Message);
                //            }

                //            var responseJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                //            await context.Response.WriteAsync(responseJson);
                //        }
                //    }
                //};
            });
        }
    }
}
