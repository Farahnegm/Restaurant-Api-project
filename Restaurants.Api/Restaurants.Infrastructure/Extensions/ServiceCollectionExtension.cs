using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfac;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authoriation;
using Restaurants.Infrastructure.Authoriation.Service;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>() // create identity endpoints for user entity
                   .AddRoles<IdentityRole>()
                   .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipleFactory>() // use custom claims principal factory
               .AddEntityFrameworkStores<RestaurantsDbContext>();// use RestaurantsDbContext as the store for Identity
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishRepository>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"));
        }
    }
}