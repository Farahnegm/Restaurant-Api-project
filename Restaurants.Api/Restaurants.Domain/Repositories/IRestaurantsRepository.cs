
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
   public interface IRestaurantsRepository // layer of abstraction 3lshan infrastructure imp. dh w w2t el runtime y resolve dh w ygyb el data
    {
        Task<IEnumerable<Restaurant>> GetAlAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> Create(Restaurant entity);
        Task DeleteAsync(Restaurant entity);
        Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize,string? SortBy , SortDirection sortDirection);
        Task SaveChanges();
    }
}
