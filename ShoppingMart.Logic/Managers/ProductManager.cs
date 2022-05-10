using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.Products;
using ShoppingMart.Domain.ViewModel;
using ShoppingMart.Infastructure.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Logic.Managers
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return products;
        }

        public async Task<ProductViewModel> GetProduct(Guid productId)
        {
            var product = await _productRepository.GetProduct(productId);
            _ = product ?? throw new ShoppingMartException("Product not found");
            return product;
        }
    }
}
