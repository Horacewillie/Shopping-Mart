using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Model
{
    public class ShoppingMartConfig
    {
        public string DbConnectionString { get; set; }

        public string WatchPageUsername { get; set; }

        public string WatchPagePassword { get; set; }
    }
}
