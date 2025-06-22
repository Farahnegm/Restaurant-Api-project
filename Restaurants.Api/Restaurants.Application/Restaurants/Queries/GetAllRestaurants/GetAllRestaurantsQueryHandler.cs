
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
    {
        public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            
            logger.LogInformation("Fetching all restaurants from the repository.");
            var restaurants = (await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase));
                                        ;

            var resturantDtos = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
            return resturantDtos!;
        }
    }
}
