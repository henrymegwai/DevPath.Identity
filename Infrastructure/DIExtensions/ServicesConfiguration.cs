using BlinkCash.Core.Configs;
using BlinkCash.Core.Interfaces.Managers;
using BlinkCash.Core.Interfaces.Repositories;
using BlinkCash.Core.Interfaces.Services;
using BlinkCash.Core.Managers;
using BlinkCash.Core.Models;
using BlinkCash.Core.Models.JwtModels;
using BlinkCash.Data.Repository;
using BlinkCash.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.DIExtensions
{
    public static class ServicesConfiguration
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            // Configuration
            services.AddMemoryCache();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<JwtTokenConfig>(configuration.GetSection("JwtTokenConfig"));
            services.AddSingleton(configuration.GetSection("AppSettings").Get<AppSettings>());
            services.AddSingleton(configuration.GetSection("JwtTokenConfig").Get<JwtTokenConfig>());


            // Services  
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<INubanService, NubanService>();
            services.AddScoped<IBvnService, BvnService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IResponseService, ResponseService>();      
            services.AddScoped<IEmailClient, SendGridEmailClient>();    
            services.AddScoped<IEmailService, EmailService>();      
            services.AddScoped<INotificationService, NotificationService>();   
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IPinService, PinService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddHttpClient();
            // Managers 
            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<ISecurityQuestionManager, SecurityQuestionManager>();
            // Repository 
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISecurityQuestionRepository, SecurityQuestionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConfirmationTokenRepository, ConfirmationTokenRepository>();

        }

        public static void AddDocumentationServices(this IServiceCollection services, string swaggerTitle = "")
        {
            services.AddSwaggerGen(c =>
            {
                string title = !string.IsNullOrEmpty(swaggerTitle) ? swaggerTitle : "BlinkCash";
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{ swaggerTitle}", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                  {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
