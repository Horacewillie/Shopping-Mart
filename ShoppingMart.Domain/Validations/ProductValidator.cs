// using FluentValidation;
// using ShoppingMart.Domain.Base;
// using ShoppingMart.Domain.ViewModel;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace ShoppingMart.Domain.Validations
// {
//
//     //I didn't inherit from InlineValidator.
//     public class ProductValidator //: AbstractValidator<ProductViewModel>
//     {
//         //public ProductValidator()
//         //{
//             
//             //RuleFor(product => product.Name).NotNull()
//             //    .MaximumLength(20);
//             //RuleFor(product => product.Price).NotEmpty()
//             //    .GreaterThan(0);
//             //RuleFor(product => product.Description).NotEmpty()
//             //    .MaximumLength(30);
//         //}
//
//         public void ValidateProductModel(ProductViewModel model)
//         {
//             var validator = new InlineValidator<ProductViewModel>();
//
//             validator.RuleLevelCascadeMode = CascadeMode.Stop;       
//                 validator.RuleFor(product => product.Name).NotNull()
//                 .MaximumLength(20);
//             validator.RuleFor(product => product.Price).NotEmpty()
//                .GreaterThan(0);
//             validator.RuleFor(product => product.Description).NotEmpty()
//                 .MaximumLength(30);
//             var validationResult = validator.Validate(model);
//             if (!validationResult.IsValid)
//                 throw new ShoppingMartException(validationResult.Errors);
//         }
//     }
// }