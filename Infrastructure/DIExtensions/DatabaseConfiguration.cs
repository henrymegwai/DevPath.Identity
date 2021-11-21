using BlinkCash.Core.Models;
using BlinkCash.Data;
using BlinkCash.Data.DapperConnection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.DIExtensions
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("BlinkCashDbContext");
            services.AddDbContextPool<AppDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });
            services.AddIdentity<IdentityUserExtension, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequireNonAlphanumeric = false,
                    RequireLowercase = false,
                    RequireUppercase = false, 
                    RequiredLength = 8, 
                };
                options.Lockout = new LockoutOptions { AllowedForNewUsers = false };

            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddEntityFrameworkSqlServer();
            services.AddTransient<IdentityDbContext<IdentityUserExtension>, AppDbContext>(); 
            services.AddTransient<IDapperContext, DapperContext>();
        }
    }
}
