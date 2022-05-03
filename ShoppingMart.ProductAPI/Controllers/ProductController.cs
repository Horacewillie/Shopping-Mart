using Microsoft.AspNetCore.Mvc;
using ShoppingMart.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingMart.ProductAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;
        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }
        public string Index()
        {
            return "I go handle you.";
        }
    }
}
