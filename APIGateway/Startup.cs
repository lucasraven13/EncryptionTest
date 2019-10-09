using System;
using APIGateway.Services.Implementations;
using APIGateway.Services.Interfaces;
using APIGateway.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using APIGateway.Middleware;
using APIGateway.Utils;

namespace APIGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddHealthChecks();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            var apiEncryptionServiceSettings = new ApiEncryptionServiceSettings();
            Configuration.Bind(nameof(apiEncryptionServiceSettings), apiEncryptionServiceSettings);
            services.AddSingleton(apiEncryptionServiceSettings);
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHostedService<KeyRotationHostedService>();
            services.AddHttpClient();
            services.AddHttpClient<IApiClient, ApiClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAll")
               .UseForwardedHeaders(new ForwardedHeadersOptions
               {
                   ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
               });
            app.UseResponseCompression();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorMiddleware>();
            app.UseMvc();
        }
    }
}
