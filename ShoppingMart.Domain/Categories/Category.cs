using ShoopingMart.Domain;
using ShoppingMart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain
{
    public class Category : DbGuidEntity
    {
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
