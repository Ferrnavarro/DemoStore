using DemoStore.API.Dtos;
using DemoStore.Core.Entities.ProductAggregate;
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

    }
}
