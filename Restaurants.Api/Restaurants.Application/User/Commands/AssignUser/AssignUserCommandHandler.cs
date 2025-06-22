
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.AssignUser
{
    public class AssignUserCommandHandler(ILogger<AssignUserCommandHandler>logger,UserManager<Domain.Entities.User> userManager , RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserCommand>
    {
        public async Task Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning role {RoleName} to user {UserEmail}", request.RoleName, request.UserEmail);
            var user = await userManager.FindByEmailAsync(request.UserEmail)
                ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);
            var role= await roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            await userManager.AddToRoleAsync(user, role.Name!);

        }
    }
}
