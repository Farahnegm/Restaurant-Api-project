using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfac;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authoriation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler>logger, IMapper mapper, IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task  Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating restaurant with ID: {RestaurantId} with {@UpdatedRestaurant}",request.Id, request);
            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
            {
                logger.LogWarning($"Restaurant with ID: {request.Id} not found.");
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new forbidException();
            }
            mapper.Map(request, restaurant); // Map the request to the restaurant entity
            await restaurantsRepository.SaveChanges();
           
        }
    }
}
