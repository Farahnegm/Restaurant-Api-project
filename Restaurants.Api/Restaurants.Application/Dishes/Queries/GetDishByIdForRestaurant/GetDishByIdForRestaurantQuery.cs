using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
   public class GetDishByIdForRestaurantQuery(int RestaurantId, int DishId) : IRequest<DishesDTO>
    {
        public int RestaurantId { get; } = RestaurantId;
        public int DishId { get; } = DishId;
    }
    
    
}
