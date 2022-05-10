using ShoppingMart.Domain;
using ShoppingMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingMart.Domain
{
    public class Product : DbGuidEntity
    {
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Guid CategoryForeignKey { get; set; }
        
        [JsonIgnore]
        public Category Category { get; set; }

        public string ImageUrl { get; set; }
    }
}
