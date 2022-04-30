using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Dtos
{
    public class ProductViewModel
    {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public Guid CategoryForeignKey { get; set; }
            public Category Category { get; set; }

            public string ImageUrl { get; set; }

    }
}
