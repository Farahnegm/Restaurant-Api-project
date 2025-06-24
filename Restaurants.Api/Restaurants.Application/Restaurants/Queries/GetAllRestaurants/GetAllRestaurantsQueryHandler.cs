
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
     IMapper mapper,
     IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDTO>>
    {
        public async Task<PageResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            var result = new PageResult<RestaurantDTO>(restaurantsDtos, totalCount, request.PageSize, request.PageNumber);
            return result;
        }
    }

}
