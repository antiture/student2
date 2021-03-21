using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student2
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next)=>
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("hello word  中文 1" );
                logger.LogInformation("MW1: 传入请求 ");
                await next();
                logger.LogInformation("MW1: 传出响应 ");
            });
            app.Use(async (context,next) =>
            {
                await context.Response.WriteAsync("hello word 2");
                logger.LogInformation("MW2: 传入请求 ");
                await next();
                logger.LogInformation("MW2: 传出响应 ");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("hello word 3");
                logger.LogInformation("MW3: 传入请求 ");
                logger.LogInformation("MW3: 传出响应 ");
            });
        }
    }
}
