using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.User.Commands.AssignUser;
using Restaurants.Application.User.Commands.UnAssignUserRole;
using Restaurants.Application.User.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

namespace Restaurants.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPatch("User")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetials([FromBody] UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();

        }
        [HttpPost("UserRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserCommand command)
        {
            await mediator.Send(command);
            return NoContent();

        }
        [HttpDelete("UnAssignRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnAssignUserRole(UnAssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();

        }
    }
}