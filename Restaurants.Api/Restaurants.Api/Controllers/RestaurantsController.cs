
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Api.Controllers
{
    [Route("api/Restaurants")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
     
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {

            var restaurants = await mediator.Send(query);
            return Ok(restaurants);

        }
        [HttpGet("id")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<ActionResult<RestaurantDTO?>> GetById(int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
             await mediator.Send(new DeleteRestaurantCommand(id));
           

            return NotFound();
        }
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)] 
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }
        [HttpPatch("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id; // Set the ID from the route parameter to the command
            await mediator.Send(command);
           
            return NotFound();

        }
    }
}