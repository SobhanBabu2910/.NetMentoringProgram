using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using CartService.BLL.DTOs;

namespace CartService.BLL.Validators
{
    internal class AddCartItemValidator:AbstractValidator<AddCartItemRequest>
    {
        public AddCartItemValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required."); ;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required."); ;
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero."); ;
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}