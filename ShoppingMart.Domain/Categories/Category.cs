using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ShoppingMart.Domain.ViewModels;

namespace ShoppingMart.Domain
{
    public class Category : DbGuidEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        public Category(CategoryViewModel model)
        :this()
        {
            model.ValidateCategoryModel();
            Name = model.Name;
           // Id = model.Id;
        }
    }
}
