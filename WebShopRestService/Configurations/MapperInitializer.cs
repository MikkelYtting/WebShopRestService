using AutoMapper;
using WebShopRestService.DTO;
using WebShopRestService.Models;

namespace WebShopRestService.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
        }
    }
}
