using AutoMapper;
using ShoopingMart.Domain;
using ShoppingMart.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Profiles
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
           {
               config.CreateMap<Product, ProductViewModel>().ReverseMap();
           });

            return mappingConfig;

        }
    }
}
