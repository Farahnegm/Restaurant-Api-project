using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Restaurants.Application.Dishes.DTOs;

namespace Restaurants.Application.Dishes.Queries.GetAllDishes
{
    public class GetAllDishesQuery(int RestaurantId): IRequest<IEnumerable<DishesDTO>>
    {
        public int RestaurantId { get; } = RestaurantId;
        
    }
}
