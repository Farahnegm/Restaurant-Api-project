using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository, IMapper mapper , ILogger<CreateRestaurantCommandHandler>logger, IUserContext userContext ): IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser= userContext.GetCurrentUser();
            logger.LogInformation("{UserEmail} [{UserId}] Create new Restaurant {@Restaurant}"
                ,currentUser.Email,
                currentUser.Id
                , request);
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;
            var id = await restaurantsRepository.Create(restaurant);
            return id;

        }
    }
}
