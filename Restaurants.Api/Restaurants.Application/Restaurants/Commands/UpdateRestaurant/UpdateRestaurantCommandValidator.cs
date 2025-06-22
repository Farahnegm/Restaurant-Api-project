using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
   public class UpdateRestaurantCommandValidator:AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(d => d.Name)
                .Length(3, 100);
            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description is required.");

        }
    }
}
