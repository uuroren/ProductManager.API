using AutoMapper;
using ProductManager.API.Dtos;
using ProductManager.API.Entities;
using ProductManager.API.Models;

namespace ProductManager.API.Mapping {
    public class MappingProfile:Profile {

        public MappingProfile() {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();
        }
    }
}
