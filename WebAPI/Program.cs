
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EfCore;
using Services.Contracts;
using WebAPI.Extensions;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.Setup().LoadConfigurationFromFile(String.Concat(Directory.GetCurrentDirectory(), "nlog.config"));
            // Add services to the container.

            builder.Services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
                .AddCustomCsvFormatter()
                .AddXmlDataContractSerializerFormatters()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                .AddNewtonsoftJson();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureLoggerService();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.ConfigureActionFilters();
            builder.Services.ConfigureCors();
            var app = builder.Build();
            var logger = app.Services.GetRequiredService<ILoggerService>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (app.Environment.IsProduction()) 
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
