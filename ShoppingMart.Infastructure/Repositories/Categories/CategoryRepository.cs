using ShoppingMart.Domain;
using ShoppingMart.Domain.Categories;
using ShoppingMart.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Repositories.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
      
        public CategoryRepository(ShoppingMartDbContext context) : base(context)
        {

        }
    }
}
