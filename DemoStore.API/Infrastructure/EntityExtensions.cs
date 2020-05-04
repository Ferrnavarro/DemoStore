using DemoStore.API.Dtos;
using DemoStore.API.Dtos.AccountDtos;
using DemoStore.Core.Entities.ProductAggregate;
using DemoStore.Core.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Infrastructure
{
    public static class EntityExtensions
    {
        public static ProductDto MapProductDto(this Product product) =>
            new ProductDto
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                NumberAvailable = product.NumberAvailable,
                PictureUri = product.PictureUri
            };

        public static Product MapProduct(this ProductDto productDto) =>
            new Product
            {
                Id = productDto.Id,
                Sku = productDto.Sku,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                NumberAvailable = productDto.NumberAvailable,
                PictureUri = productDto.PictureUri
            };

        public static Product MapNewProduct(this NewProductDto newProductDto) =>
            new Product
            {             
                Sku = newProductDto.Sku,
                Name = newProductDto.Name,
                Description = newProductDto.Description,
                Price = newProductDto.Price,
                NumberAvailable = newProductDto.NumberAvailable,              
            };

        public static ApplicationUser MapNewApplicationUser(this NewUserDto newUserDto) =>
            new ApplicationUser
            {
                Name = newUserDto.Name,
                PhoneNumber = newUserDto.PhoneNumber,
                UserName = newUserDto.UserName,
                BirthDate = newUserDto.BirthDate,
                Email = newUserDto.Email
            };

        public static UserDto MapUserDto(this ApplicationUser applicationUser) =>
            new UserDto
            {
                Id = applicationUser.Id,
                Name = applicationUser.Name,
                PhoneNumber = applicationUser.PhoneNumber,
                UserName = applicationUser.UserName,
                BirthDate = applicationUser.BirthDate,
                Email = applicationUser.Email
            };


    }
}
