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
        //TODO: Terminar de mapear endpoints
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("products")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProductsListAsync();

            _logger.LogInformation($"Success retrieving {products?.Count()} products");
            return Ok(products);
        }

        [HttpGet("product/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if(product is null)
            {
                _logger.LogError($"Product with id '{id}' not found");
                return NotFound();
            }

            _logger.LogInformation($"Success in retrieving product with id '{id}'");
            return Ok(product);
        }

        [HttpGet("category/{categoryName}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
            var products = await _repository.GetProductsListByCategoryAsync(categoryName);

            if (products is null || !products.Any())
            {
                _logger.LogError($"Category '{categoryName}' products not found");
                return NotFound();
            }

            _logger.LogInformation($"Success in recovering category '{categoryName}' products");
            return Ok(products);
        }
    }
}
