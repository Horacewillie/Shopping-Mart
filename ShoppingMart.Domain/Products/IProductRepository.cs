using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Products
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<ProductViewModel>> GetProducts();

        Task<ProductViewModel> GetProduct(Guid productId);

        //Task<ProductViewModel> CreateUpdateProduct(ProductViewModel productViewModel);

        Task<bool> DeleteProduct(Guid productId);
    }
}
