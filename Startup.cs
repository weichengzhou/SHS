using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using SHS.Controllers.Exceptions;
using SHS.Services;
using SHS.Repositories;
using SHS.Models;


namespace SHS
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
            // DI : Service Layer
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<IApiExceptionHandler, ShsApiExceptionHandler>();
            // DI : Repository Layer
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAgentRepo, AgentRepo>();
            // DI : Model Layer
            services.AddScoped<DbContext, ShsDbContext>();
            string connectionStr = Configuration.GetConnectionString("ShsMySql");
            services.AddDbContext<ShsDbContext>(options => {
                options.UseMySql(connectionStr, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.14-mariadb"));
            });
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            services.AddApiVersioning();
            services.AddSwaggerGen(c => {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger(c =>
            {
                // Decide the path where swagger json file located.
                c.RouteTemplate = "docs/{documentName}/docs.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/docs/v1/docs.json", "SHS API V1.0");
            });
        }
    }
}
