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

namespace ShoppingMart.Logic.Managers
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProductManager(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
            var product = _mapper.Map<ProductViewModel, Product>(model);
            var category = await _categoryRepository.GetCategoryName(model.Category.Name);
            category.Products.Add(product);
            await _categoryRepository.SaveChangesAsync();
            ProductViewModel savedProduct = _mapper.Map<Product, ProductViewModel>(product);
            return Envelope.Ok(savedProduct);
        }

        public async Task<Envelope<ProductViewModel>> UpdateProduct(ProductViewModel model)
        {
            model.ValidateProductModel();
            var product = new Product(new ProductViewModel
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Category = model.Category
            });
            _productRepository.UpdateEntity(product);
            await _productRepository.SaveChangesAsync();
            var updatedProduct = new ProductViewModel(product);
            return Envelope.Ok(updatedProduct);
        }
    }
}
