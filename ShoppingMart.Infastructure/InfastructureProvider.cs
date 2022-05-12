using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingMart.Domain.Products;
using ShoppingMart.Infastructure.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingMart.Domain.Categories;
using ShoppingMart.Infastructure.Repositories.Categories;

namespace ShoppingMart.Infastructure
{
    public class InfastructureProvider
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration, bool preferTransient = false)
        {
            if (preferTransient)
            {
                services.AddTransient<IProductRepository, ProductRepository>();
                services.AddTransient<ICategoryRepository, CategoryRepository>();
            }
            else
            {
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
            }
        }
    }
}
