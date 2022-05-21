using ShoppingMart.Domain;
using ShoppingMart.Domain.Categories;
using ShoppingMart.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingMart.Domain.ViewModels;

namespace ShoppingMart.Infastructure.Repositories.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
      
        public CategoryRepository(ShoppingMartDbContext context) : base(context)
        {

        }
        
        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            Category category = await DbContext.Categories.Where(c => c.Id == categoryId)
                .Include(c => c.Products)
                .FirstOrDefaultAsync();
            return category;
        }

        public async Task<Category> GetCategoryName(string name)
        {
            Category category = await DbContext.Categories.Where(c => c.Name == name)
                .Include(c => c.Products)
                .FirstOrDefaultAsync();
            //Console.WriteLine(DbContext.Entry(category));
            return category;
        }
    }
}
