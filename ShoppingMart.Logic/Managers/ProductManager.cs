using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.Products;
using ShoppingMart.Domain.ViewModels;
using ShoppingMart.Infastructure.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShoppingMart.Domain.Categories;
using ShoppingMart.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShoppingMart.Logic.Managers
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ShoppingMartDbContext _context;

        public ProductManager(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper, ShoppingMartDbContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _context = context;
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

        public  async Task<Envelope<ProductViewModel>> CreateProduct(ProductViewModel model)
        {
            model.ValidateProductModel();
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Description = model.Description
            };

            _productRepository.AddEntity(product);
            var category = await _categoryRepository.GetCategoryName(model.Category.Name);
            Console.WriteLine(_context.Entry(product).Context);
            Console.WriteLine(_context.Entry(category).Context); 
            //_context.Entry(product).State = EntityState.Added;
            category.Products.Add(product);
            await _categoryRepository.SaveChangesAsync();
            ProductViewModel savedProduct = _mapper.Map<Product, ProductViewModel>(product);
            return Envelope.Ok(savedProduct);
        }

        public async Task<Envelope<ProductViewModel>> UpdateProduct(ProductViewModel model)
        {
            var product = await _productRepository.FindEntityAsync(model.Id);
            if (product == null)
                throw new ShoppingMartException("Product not found");
            product.EditProduct(model);
            var state = _context.Entry(product).State;
            Console.WriteLine(state);
            _productRepository.UpdateEntity(product);
            
            await _productRepository.SaveChangesAsync();
            var productDto = new ProductViewModel(product);
            return Envelope.Ok(productDto);
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            bool deleted = await _productRepository.DeleteProduct(productId);
            //_ = product ?? throw new ShoppingMartException("Product not found");
            return deleted;
        }
    }
}
