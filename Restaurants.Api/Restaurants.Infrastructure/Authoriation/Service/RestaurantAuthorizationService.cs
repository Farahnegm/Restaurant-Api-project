
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfac;

namespace Restaurants.Infrastructure.Authoriation.Service
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser()!;
            logger.LogInformation("Authorize user {UserEmail}, to {Operation} for {RestaurantName}", user.Email, resourceOperation, restaurant.Name);
            if (resourceOperation == ResourceOperation.Create || resourceOperation == ResourceOperation.Read)
            {
                logger.LogInformation("User {UserEmail} is authorized to {Operation} for {RestaurantName}", user.Email, resourceOperation, restaurant.Name);
                return true;

            }
            if (resourceOperation == ResourceOperation.Delete || user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("User {UserEmail} is authorized to {Operation} for {RestaurantName}", user.Email, resourceOperation, restaurant.Name);
                return true;
            }
            if (resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Create && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("User {UserEmail} is authorized to {Operation} for {RestaurantName}", user.Email, resourceOperation, restaurant.Name);
                return true;
            }
            return false;
        }
    }
}
