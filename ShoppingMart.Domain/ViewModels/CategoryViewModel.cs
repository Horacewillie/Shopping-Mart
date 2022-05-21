using FluentValidation;
using ShoppingMart.Domain.Base;
using System;

namespace ShoppingMart.Domain.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        //public Guid Id { get; set; }
        public void ValidateCategoryModel()
        {
            var validator = new InlineValidator<CategoryViewModel>();
            validator.RuleLevelCascadeMode = CascadeMode.Stop;
            validator.RuleFor(product => product.Name).NotNull()
                .MaximumLength(20);
            var validationResult = validator.Validate(this);
            if (!validationResult.IsValid)
                throw new ShoppingMartException(validationResult.Errors);
        }
    }
}