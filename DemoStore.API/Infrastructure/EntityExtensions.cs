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

            };

        public static Product MapProduct(this ProductDto productDto) =>
            new Product
            {

            };


    }
}
