using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using ShoppingMart.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingMart.Domain
{
    public class Product : DbGuidEntity
    {
        public Product()
        {

        }
       
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryForeignKey { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public string ImageUrl { get; set; }


        public void EditProduct(ProductViewModel model)
        {
            model.ValidateProductModel();
            Name = model.Name;
            Price = model.Price;
            ImageUrl = model.ImageUrl;
            Description = model.Description;
        }
    }
}
