using E_Commerce_API.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please add product name")
                .MaximumLength(150)
                .MinimumLength(3)
                .WithMessage("Please add 3-150 character word");
            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please add stock count")
                .Must(x => x >= 0)
                .WithMessage("Please add positive number");
            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please add price count")
                .Must(x => x >= 0)
                .WithMessage("Please add positive number");
        }
    }
}
