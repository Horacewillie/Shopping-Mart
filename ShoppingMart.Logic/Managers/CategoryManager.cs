using System.Threading.Tasks;
using AutoMapper;
using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.Categories;
using ShoppingMart.Domain.ViewModels;

namespace ShoppingMart.Logic.Managers
{
    public class CategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        
        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Envelope<Category>> CreateCategory(CategoryViewModel model)
        {
            model.ValidateCategoryModel();
            var category = _mapper.Map<CategoryViewModel, Category>(model);
            _categoryRepository.AddEntity((category));
            await _categoryRepository.SaveChangesAsync();
            var savedCategory = new Category
            {
                Name = model.Name
            };
            return Envelope.Ok(savedCategory);
        }
    }
}