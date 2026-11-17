using FluentValidation;
using RestfulCartService.Domain.Entities.DTOs;

namespace RestfulCartService.Application.Validator
{
    public class AddCartItemValidator: AbstractValidator<AddCartItemRequest>
    {
        public AddCartItemValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
