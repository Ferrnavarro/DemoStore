using DemoStore.API.Dtos;
using DemoStore.API.Infrastructure;
using DemoStore.API.Interfaces;
using DemoStore.Core.Entities.ProductAggregate;
using DemoStore.Core.Interfaces;
using DemoStore.Core.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IAsyncRepository<Product> productRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }



        public async Task<ProductDto> AddProductAsync(ProductDto productDto, HttpRequest httpRequest)
        {
            var product = productDto.MapProduct();

            if (productDto.PictureToUpload.Length > 0)
            {
                product.PictureUri = await _fileService.SaveFileAsync(productDto.PictureToUpload, httpRequest);
            }

            await _productRepository.AddAsync(product);

            return product.MapProductDto();
        }

        public Task<int> CountProductAsync(int pageIndex = 0, int itemsPage = 10, string orderBy = "title", string search = "")
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(ProductDto productDto)
        {
           await _productRepository.DeleteAsync(productDto.MapProduct());
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product != null)
            {
                return product.MapProductDto();
            }

            return null;

        }

        public async Task<IReadOnlyList<ProductDto>> ListAllProductAsync()
        {
            var products = await _productRepository.ListAllAsync();
            return products.Select(s => s.MapProductDto()).ToList();                  
        }      

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
        {
            var product = productDto.MapProduct();
            await _productRepository.UpdateAsync(product);

            return await GetProductByIdAsync(productDto.Id);
        }

        public async Task<ProductCatalogDto> ListProductsAsync(int pageIndex, int itemsPage, string orderBy, string search)
        {
            var filterPaginatedEspecification = new ProductFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, orderBy, search);
            var filterSpecification = new ProductFilterSpecification(orderBy, search);

            var itemsOnPage = await _productRepository.ListAsync(filterPaginatedEspecification);
            var totalItems = await _productRepository.CountAsync(filterSpecification);

            var products = new ProductCatalogDto()
            {
                Products = itemsOnPage.Select(s => s.MapProductDto()).ToList(),

                PaginationInfo = new PaginationInfoDto()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            products.PaginationInfo.Next = (products.PaginationInfo.ActualPage == products.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            products.PaginationInfo.Previous = (products.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return products;
        }
    }
}
