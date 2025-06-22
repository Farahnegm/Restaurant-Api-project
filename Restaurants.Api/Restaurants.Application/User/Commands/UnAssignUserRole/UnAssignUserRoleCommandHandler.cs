using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UnAssignUserRole
{
    public class UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler>logger, UserManager<Domain.Entities.User>userManager,RoleManager<IdentityRole>roleManager ) : IRequestHandler<UnAssignUserRoleCommand>
    {
        public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        { 
            logger.LogInformation("UnAssigning role {RoleName} from user {UserEmail}", request.RoleName, request.UserEmail);
            var user = await userManager.FindByEmailAsync(request.UserEmail)
                
                ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);
            var role = await roleManager.FindByNameAsync(request.RoleName);
            if (user == null || role == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);
            }
            await userManager.RemoveFromRoleAsync( user, role.Name!);

        }
    }
}
