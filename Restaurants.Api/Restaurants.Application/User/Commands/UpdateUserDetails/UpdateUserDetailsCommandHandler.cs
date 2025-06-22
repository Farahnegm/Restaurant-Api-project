using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext , IUserStore<Domain.Entities.User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user= userContext.GetCurrentUser();
            logger.LogInformation("Updating user details for user {UserId} with {@request}", user!.Id, request);
            var dbuser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
            if (dbuser == null)
            {
                logger.LogWarning("User with ID {UserId} not found", user!.Id);
                throw new NotFoundException(nameof(user), user!.Id);
            }
            dbuser.DateOfBirth = request.DateOfBirth;
            dbuser.Nationality  = request.Nationality;
     await userStore.UpdateAsync(dbuser, cancellationToken);
          
        }
    }
}
