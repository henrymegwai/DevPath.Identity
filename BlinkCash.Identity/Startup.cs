using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlinkCash.Infrastructure.DIExtensions;
using Hangfire;
using Hangfire.SqlServer;
using BlinkCash.Identity.Filters;

namespace BlinkCash.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }
        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            Configuration = config;
            ConfigurationRoot = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDatabaseServices(Configuration);
            services.AddSecurityServices(Configuration);
            services.AddAppServices(Configuration);
            services.AddDocumentationServices("BlinkCash.Identity.Api");
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                                                               .UseSimpleAssemblyNameTypeSerializer()
                                                               .UseRecommendedSerializerSettings()
                                                               .UseSqlServerStorage(Configuration
                                                               .GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                                                               {
                                                                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                                                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                                                   QueuePollInterval = TimeSpan.Zero,
                                                                   UseRecommendedIsolationLevel = true,
                                                                   UsePageLocksOnDequeue = true,
                                                                   DisableGlobalLocks = true,
                                                                   PrepareSchemaIfNecessary = true
                                                               }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseHangfireDashboard("/scheduledashboard", new DashboardOptions { Authorization = new[] { new MyAuthorizationFilter() } });
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlinkCash.Identity.Api v1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
