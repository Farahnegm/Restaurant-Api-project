using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authoriation;

namespace Restaurants.Domain.Interfac
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}