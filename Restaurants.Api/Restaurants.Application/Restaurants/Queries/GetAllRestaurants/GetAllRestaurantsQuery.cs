
using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
   public class GetAllRestaurantsQuery:IRequest<PageResult<RestaurantDTO>>
    {
       public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

    }
}
