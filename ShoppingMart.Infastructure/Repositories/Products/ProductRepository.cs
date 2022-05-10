using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingMart.Domain;
using ShoppingMart.Domain.ViewModel;
using ShoppingMart.Domain.Products;
using ShoppingMart.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Repositories.Products
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        protected readonly IMapper _mapper;
        public ProductRepository(ShoppingMartDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            try
            {
                Product product = await DbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product is null) return false;
                DbContext.Products.Remove(product);
                await DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductViewModel> GetProduct(Guid productId)
        {
            Product product = await DbContext.Products
                .Where(p => p.Id == productId && p.Deleted != true)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            List<Product> productList = await DbContext.Products
                .Where(p => p.Deleted != true)
                .Include(p => p.Category).ToListAsync();
            var productViewModel = _mapper.Map<List<ProductViewModel>>(productList);
            return productViewModel;
        }
    }
}
