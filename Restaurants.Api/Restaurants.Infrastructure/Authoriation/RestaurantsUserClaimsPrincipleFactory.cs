﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.Infrastructure.Authoriation
{
    public class RestaurantsUserClaimsPrincipleFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options
        ) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user);
            if (user.Nationality != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));

            }
            if (user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }
            return new ClaimsPrincipal(id);
        }
    }
}