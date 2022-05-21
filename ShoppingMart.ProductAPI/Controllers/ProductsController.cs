using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.ViewModels;
using ShoppingMart.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMart.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly ProductManager _productManager;
        public ProductsController(ILogger logger, ProductManager productManager)
            : base(logger)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [Produces(typeof(Envelope<List<ProductViewModel>>))]
        public async Task<IActionResult> GetProduct()
        {
            try
            {
                List<ProductViewModel> products = await _productManager.GetProducts();
                return Ok(products);
            }
            catch (ShoppingMartException sx)
            {
                return Failure(sx.Errors);
            }
            catch(Exception ex)
            {
                LogError("Error occured while getting products", ex);
                return Failure($"An error occured | {ex.Message}");
            }
        }
        
        [HttpGet("{productId}")]
        [Produces(typeof(Envelope<ProductViewModel>))]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                var product = await _productManager.GetProduct((productId));
                //if(product is null) return NotFound()
                return Ok(product);
            }
            catch (ShoppingMartException sx)
            {
                return NotFoundError(sx.Message);
            }
            catch (Exception ex)
            {
                LogError("Error occured while fetching product", ex);
                return Failure($"An error occured | {ex.Message}");
            }
        }
        
        [HttpPost("Create")]
        [Produces(typeof(Envelope<ProductViewModel>))]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            try
            {
                var product = await _productManager.CreateProduct(model);
                return Done(product);
            }
            catch (ShoppingMartException sx)
            {
                LogError("Error occured while attempting to create product:", sx, model);
                return Failure(sx.Errors);
            }
            catch (Exception ex)
            {
                LogError("Product Creation error occured", ex);
                return Failure($"An error occured | {ex.Message}");
            }
        }

        [HttpPost("Edit")]
        [Produces(typeof(Envelope<ProductViewModel>))]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            try
            {
                var product = await _productManager.UpdateProduct(model);
                return Done(product);
            }
            catch (ShoppingMartException sx)
            {
                return Failure((sx.Errors));
            }
            catch (Exception ex)
            {
                LogError("Product Update error occured", ex);
                return Failure($"An error occured | {ex.Message}");
            }
        }

        [HttpDelete("{productId}")]
        [Produces(typeof(Envelope<bool>))]
        public async Task<IActionResult> Delete(Guid productId)
        {
            try
            {
                var result = await _productManager.DeleteProduct(productId);
                if (!result) return NotFoundError("Item not found for deletion!"); 
                return Ok(result);
            }
            catch (ShoppingMartException sx)
            {
                return Failure((sx.Errors));
            }
            catch (Exception ex)
            {
                LogError("Product Deletion error occured", ex);
                return Failure($"An error occured | {ex.Message}");
            }
        }

    }
}
