using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
   public class CreateRestaurantCommandValidator: AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> CategoryList = ["Italian", "Mexican", "Japanese", "American", "Indian"]; 
        public CreateRestaurantCommandValidator() 
        {
            RuleFor(d => d.Name)
                .Length(3, 100);
            RuleFor(d => d.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(d => d.Category)
                .Must(CategoryList.Contains)
                .WithMessage("InValid Category, choose one from the valis categories");
            RuleFor(d => d.ContactEmail)
                .EmailAddress()
                .WithMessage("Invalid email address format.");
            RuleFor(d => d.ContactNumber)
                .Matches(@"^\+?[0-9\s\-\(\)]+$")
                .WithMessage("Invalid phone number format.");
           RuleFor(d => d.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Invalid postal code format. Use XX-XXX format.");
        }
    }
}
