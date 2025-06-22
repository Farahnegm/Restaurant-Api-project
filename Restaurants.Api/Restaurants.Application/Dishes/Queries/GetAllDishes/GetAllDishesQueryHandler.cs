using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQueryHandler(IRestaurantsRepository restaurantsRepository, ILogger<GetAllDishesQueryHandler> logger, IMapper mapper, IDishesRepository dishesRepository) : IRequestHandler<GetAllDishesQuery, IEnumerable<DishesDTO>>
    {
        public async Task<IEnumerable<DishesDTO>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
           logger.LogInformation("Fetching all dishes for restaurant with id :{RestaurantId}", request.RestaurantId);
            var restaurant =await restaurantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
                
            }
            var results =mapper.Map<IEnumerable<DishesDTO>>(restaurant.Dishes);
            return results;
        }
    }
}
