using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.persistence;

namespace Restaurants.Infrastructure.Seeders
{
    public class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
                [
                new(UserRoles.User){
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Owner){
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new(UserRoles.Admin){
                    NormalizedName = UserRoles.Admin.ToUpper() 
                }
                ];
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
    {
        new Restaurant
        {
            Name = "Tasty Bites",
            Description = "Delicious food for every mood.",
            Category = "Fast Food",
            HasDelivery = true,
            ContactEmail = "info@tastybites.com",
            ContactNumber = "01012345678",
            Address = new Address
            {
                Street = "12 Downtown St",
                City = "Cairo",
                PostalCode = "11234"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Cheeseburger",
                    Description = "Juicy beef with melted cheese.",
                    Price = 55.0m
                },
                new Dish
                {
                    Name = "Fries",
                    Description = "Crispy golden fries.",
                    Price = 20.0m
                }
            }
        },
        new Restaurant
        {
            Name = "Green Garden",
            Description = "Fresh and healthy vegetarian meals.",
            Category = "Vegetarian",
            HasDelivery = false,
            ContactEmail = "contact@greengarden.com",
            ContactNumber = "01098765432",
            Address = new Address
            {
                Street = "8 Nile St",
                City = "Giza",
                PostalCode = "12655"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Grilled Veggie Plate",
                    Description = "Mixed seasonal vegetables grilled to perfection.",
                    Price = 65.0m
                },
                new Dish
                {
                    Name = "Lentil Soup",
                    Description = "Warm and comforting.",
                    Price = 35.0m
                }
            }
        },
        new Restaurant
        {
            Name = "Casa Pasta",
            Description = "Authentic Italian pasta dishes.",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "hello@casapasta.com",
            ContactNumber = "01111222333",
            Address = new Address
            {
                Street = "5 Pasta Lane",
                City = "Alexandria",
                PostalCode = "21523"
            },
            Dishes = new List<Dish>
            {
                new Dish
                {
                    Name = "Spaghetti Bolognese",
                    Description = "Rich meat sauce over spaghetti.",
                    Price = 75.0m
                },
                new Dish
                {
                    Name = "Fettuccine Alfredo",
                    Description = "Creamy white sauce with parmesan.",
                    Price = 80.0m
                }
            }
        }
    };

            return restaurants;
        }

    }
}