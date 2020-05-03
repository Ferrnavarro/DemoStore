using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoStore.API.Dtos;
using DemoStore.API.Interfaces;
using DemoStore.Core.Entities.ProductAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ProductCatalogDto>> Get(int pageIndex = 0, int itemsPage = 10, string orderBy = "name", string search = "")
        {
            var productCatalog = await _productService.ListProductsAsync(pageIndex, itemsPage, orderBy, search);

            return productCatalog;
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = await _productService.AddProductAsync(productDto, HttpContext.Request);
                return CreatedAtAction("Get", new { id = product.Id }, product);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != productDto.Id)
                {
                    return BadRequest("Id does not match Id in product object");
                }

                var productInDb = await _productService.GetProductByIdAsync(id);

                if (productInDb == null)
                {
                    return NotFound();
                }

               var productUpdated =  await _productService.UpdateProductAsync(productDto);


                return Ok(new { successMessage = "Product updated", product = productUpdated });

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");       
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(product);

            return product;
        }
    }
}
