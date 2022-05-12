
using System.Text.Json.Serialization;
using FluentValidation;
using ShoppingMart.Domain.Base;

namespace ShoppingMart.Domain.ViewModels

{
    public class ProductViewModel
    {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public virtual CategoryViewModel Category { get; set; }
            public string ImageUrl { get; set; }

            public ProductViewModel()
            {
                    
            }

            public ProductViewModel(Product product)
            {
                if (product is null) return;
                Name = product.Name;
                Price = product.Price;
                ImageUrl = product.ImageUrl;
                Description = product.Description;
            }
            
            public void ValidateProductModel()
            {
                var validator = new InlineValidator<ProductViewModel>();

                validator.RuleLevelCascadeMode = CascadeMode.Stop;       
                validator.RuleFor(product => product.Name).NotNull()
                    .MaximumLength(20);
                validator.RuleFor(product => product.Price).NotEmpty()
                    .GreaterThan(0);
                validator.RuleFor(product => product.Description).NotEmpty()
                    .MaximumLength(30);
                validator.RuleFor(product => product.Category).NotEmpty();
                var validationResult = validator.Validate(this);
                if (!validationResult.IsValid)
                    throw new ShoppingMartException(validationResult.Errors);
            }

    }
}
