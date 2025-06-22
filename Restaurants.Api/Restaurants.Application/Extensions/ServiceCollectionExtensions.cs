
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.User;


namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)

        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));

            services.AddAutoMapper(applicationAssembly);
           services.AddHttpContextAccessor();
           services.AddScoped<IUserContext, UserContext>();


            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        }
    }
}