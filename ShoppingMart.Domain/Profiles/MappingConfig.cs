using AutoMapper;
using ShoppingMart.Domain.ViewModels;


namespace ShoppingMart.Domain.Profiles
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMap()
        {
            var mappingConfig = new MapperConfiguration(config =>
           {
               config.CreateMap<Product, ProductViewModel>().ReverseMap();
               config.CreateMap<Category, CategoryViewModel>().ReverseMap();
           });

            return mappingConfig;

        }
    }
}
