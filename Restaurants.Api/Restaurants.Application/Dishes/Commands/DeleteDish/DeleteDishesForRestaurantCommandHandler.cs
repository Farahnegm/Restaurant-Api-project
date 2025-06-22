
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfac;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authoriation;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
          logger.LogWarning("Delete all dishes for restaurant with id {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            {
                throw new forbidException();
            }
            await dishesRepository.DeleteDishes(restaurant.Dishes);
        }
    }
}
