using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity); 
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteDishes(IEnumerable<Dish> entities)
        {
           dbContext.Dishes.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
