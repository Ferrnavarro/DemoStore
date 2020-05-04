using DemoStore.API.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);

        Task<ProductDto> GetProductByIdAsyncAsNoTracking(int id);
        Task<IReadOnlyList<ProductDto>> ListAllProductAsync();
        Task<ProductCatalogDto> ListProductsAsync(int pageIndex = 0, int itemsPage = 10, string orderBy = "name", string search = "");
        Task<ProductDto> AddProductAsync(NewProductDto productDto, HttpRequest httpRequest);
        Task<ProductDto> UpdateProductAsync(ProductDto productDto, HttpRequest httpRequest);
        Task DeleteProductAsync(ProductDto productDto);
        Task<int> CountProductAsync(int pageIndex = 0, int itemsPage = 10, string orderBy = "name", string search = "");
    }
}
