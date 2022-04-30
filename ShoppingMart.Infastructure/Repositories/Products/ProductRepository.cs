using ShoopingMart.Domain;
using ShoppingMart.Domain.Dtos;
using ShoppingMart.Domain.Products;
using ShoppingMart.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Repositories.Products
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        //Constructor
        public ProductRepository(ShoppingMartDbContext context) : base(context)
        {

        }

        public Task<ProductViewModel> CreateUpdateProduct(ProductViewModel productViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> GetProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
