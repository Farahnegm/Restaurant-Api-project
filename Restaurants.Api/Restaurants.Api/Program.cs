

using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.persistence;
using Restaurants.Application.Extensions;
using Serilog;
using Serilog.Events;
using Restaurants.Api.MiddleWare;
using Restaurants.Api.MiddleWares;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Domain.Entities;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Restaurants.Api.Extensions;


namespace Restaurants.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ErrorHandlingMiddleWare>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleWare>();

            builder.AddPresentationServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(context.Configuration));


            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();
            app.UseMiddleware<ErrorHandlingMiddleWare>();
            app.UseMiddleware<RequestTimeLoggingMiddleWare>(); 
            app.UseSerilogRequestLogging(); //It automatically logs every Request and Response going in and out of the API

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
               
            }

            app.UseHttpsRedirection();
            app.MapGroup("api/identity")
               .WithTags("Identity")
               .MapIdentityApi<User>();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
