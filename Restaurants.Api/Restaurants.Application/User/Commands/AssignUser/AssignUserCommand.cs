using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.User.Commands.AssignUser
{
   public class AssignUserCommand:IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } =default!;
      
    }
}
