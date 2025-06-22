
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
   public interface IRestaurantsRepository // layer of abstraction 3lshan infrastructure imp. dh w w2t el runtime y resolve dh w ygyb el data
    {
        Task<IEnumerable<Restaurant>> GetAlAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> Create(Restaurant entity);
        Task DeleteAsync(Restaurant entity);
        Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);
        Task SaveChanges();
    }
}
