using AutoMapper;
using OnlineShopping.Database.Entity;
using OnlineShopping.Model.Model;

namespace OnlineShopping.Service.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductModel>()
                .ReverseMap();
        }
    }
}
