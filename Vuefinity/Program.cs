using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Vuefinity.Data;
using Vuefinity.Services.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace Vuefinity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline
            Configure(app, builder.Environment);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Vuefinity API",
                    Description = "API for game data",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Annette & Erling",
                        Url = new Uri("https://example.com/contact")
                    }
                });

                // Get the XML comments file
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                // Include XML comments in Swagger
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            });

            services.AddDbContext<VuefinityDdContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped<IUserService, UserService>();

            // Add AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var MyAllowSpesificOrigins = "_myAllowSpesificOrigins";

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpesificOrigins, policy =>
                {
                    policy.WithOrigins(
                        "https://vuefinity20231121154528.azurewebsites.net",
                        "http://localhost:8080")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

        }



        private static void Configure(WebApplication app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable Swagger in development
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });

                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }


            app.UseHttpsRedirection();
            app.UseCors();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
