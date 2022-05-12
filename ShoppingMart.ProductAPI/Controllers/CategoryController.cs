using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.ViewModels;
using ShoppingMart.Logic.Managers;

namespace ShoppingMart.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly CategoryManager _categoryManager;
        public CategoryController(ILogger logger, CategoryManager categoryManager)
            : base(logger)
        {
            _categoryManager = categoryManager;
        }
        
        [HttpPost("Create")]
        [Produces(typeof(Envelope<Category>))]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            try
            {
                var category = await _categoryManager.CreateCategory(model);
                return Done(category);
            }
            catch (ShoppingMartException sx)
            {
                LogError("Error occured while attempting to create category:", sx, model);
                return Failure(sx.Errors);
            }
            catch (Exception ex)
            {
                LogError("Category Creation error occured", ex);
                return Failure($"An error occured | {ex.Message}");
            }
        }
    }
}