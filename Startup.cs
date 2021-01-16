using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SpiralWorksWalletBackendExam.DataContexts;
using SpiralWorksWalletBackendExam.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam
{
    public class Startup
    {
        private readonly string _spiralEWalletDatabaseConnectionString = "DevelopmentSpiralEWallet";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AutoRegisterByInterfaceName("ICreate");
            services.AutoRegisterByNameSpace("DataServices");
            services.AddDbContext<SpiralEWalletContext>(options => options.UseLazyLoadingProxies()
         .UseSqlServer(
        Configuration.GetConnectionString(_spiralEWalletDatabaseConnectionString),
        sqlServerOptions => sqlServerOptions.CommandTimeout(999)
        ));
            var issuer = Configuration.GetSection("TokenAuthentication:Issuer").Value;
            var audience = Configuration.GetSection("TokenAuthentication:Audience").Value;
            var tokenKey = Configuration["AppSettings:Token"];
            services.AddAuthenticationMethods(issuer, audience, tokenKey);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Spiral E-Wallet REST API",
                        Version = "v1"
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);
            });
            services.ConfigureCors();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spiral E-Wallet API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseCors("AllowSpecificOrigin");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
