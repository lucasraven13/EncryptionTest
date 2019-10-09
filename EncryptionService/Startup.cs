using EncryptionService.Middleware;
using EncryptionService.Services.Implementations;
using EncryptionService.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EncryptionService
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
            services.AddScoped<IEncryptionService, EncryptingService>();
            services.AddSingleton<IEncryptionKeyService, EncryptionKeyService>();
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
