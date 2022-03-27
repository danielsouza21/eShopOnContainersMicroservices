using Catalog.Domain.Entities;
using Catalog.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProductsListAsync();

            _logger.LogInformation($"Success retrieving {products?.Count()} products");
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if(product is null)
            {
                _logger.LogWarning($"Product with id '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Success in retrieving product with id '{id}'");
            return Ok(product);
        }

        [HttpGet("[action]/{categoryName}", Name = "GetProductByCategory")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
            var products = await _repository.GetProductsListByCategoryAsync(categoryName);

            if (products is null || !products.Any())
            {
                _logger.LogWarning($"Category '{categoryName}' products not found");
                return NotFound();
            }

            _logger.LogInformation($"Success in recovering category '{categoryName}' products");
            return Ok(products);
        }

        [HttpGet("[action]/{productName}", Name = "GetProductByName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string productName)
        {
            var product = await _repository.GetProductByNameAsync(productName);

            if (product is null)
            {
                _logger.LogWarning($"Product '{productName}' products not found");
                return NotFound();
            }

            _logger.LogInformation($"Success in recovering product '{productName}'");
            return Ok(product);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProductAsync(product);

            _logger.LogInformation($"Success adding new product {product.Name}.");
            return CreatedAtRoute("GetProduct", new { product.Id }, product);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> UpdateProduct([FromBody] Product product)
        {
            var updateResult = await _repository.UpdateProductWithIdAsync(product);

            _logger.LogInformation($"Success in updating product {product.Name}.");
            return Ok(new { updateResult });
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> DeleteProduct(string id)
        {
            var deleteResult = await _repository.DeleteProductByIdAsync(id);

            _logger.LogInformation($"Success in deleting product with id '{id}'.");
            return Ok(new { deleteResult });
        }
    }
}
