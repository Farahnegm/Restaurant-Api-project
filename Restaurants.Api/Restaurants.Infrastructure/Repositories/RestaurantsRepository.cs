using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.persistence;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository  // 3mlt el repo dh 3lshan y access el dbContext 
    {
        public async Task<int> Create(Restaurant entity)
        {
           dbContext.Restaurants.Add(entity); // add the restaurant entity to the dbContext
           await  dbContext.SaveChangesAsync();
            return entity.id;
        }

        public Task DeleteAsync(Restaurant entity)
        {
            dbContext.Restaurants.Remove(entity);
            return dbContext.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Restaurant>> GetAlAsync()
        {
            var restaurants = await dbContext.Restaurants. ToListAsync();
            return restaurants;
        }
        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase,
         int pageSize,
         int pageNumber ,
            string? sortBy,
           SortDirection sortDirection
            )
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext
                .Restaurants
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                       || r.Description.ToLower().Contains(searchPhraseLower)));

            var totalCount = await baseQuery.CountAsync();
            if (sortBy != null)

            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }
        
            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(r=> r.Dishes)
                    .FirstOrDefaultAsync(x => x.id == id);
            return restaurant;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}