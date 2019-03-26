using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Data;
using AutoMapper;
using Serilog;
using Serilog.Settings.Configuration;
using Serilog.Sinks.File;

namespace PrimerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env )
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($"E:/Projects/Log Files/AureliaLog.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                 outputTemplate: "{ Timestamp: yyyy - MM - dd HH: mm: ss.fff zzz} [{Level}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }
               
        //public IConfiguration Configuration { get; set; }
        //public static string ConnectionString { get; private set; }

      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("ConfigureServices called.");

            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://www.ids.com"; //promeniti pre deploya
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api1";
                });

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Information("Configure called.");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            //za json
            //ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
    }
}
