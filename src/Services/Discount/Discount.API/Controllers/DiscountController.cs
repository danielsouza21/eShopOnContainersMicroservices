using Discount.Domain.Entities;
using Discount.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IDiscountRepository repository, ILogger<DiscountController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetProducts(string productName)
        {
            var couponDiscount = await _repository.GetDiscountAsync(productName);

            _logger.LogInformation($"Success retrieving {productName} coupon discount");
            return Ok(couponDiscount);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscountAsync(coupon);

            _logger.LogInformation($"Success creating {coupon.ProductName} coupon");
            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateBasket([FromBody] Coupon coupon)
        {
            var updateResult = await _repository.UpdateDiscountAsync(coupon);

            if (!updateResult)
            {
                _logger.LogWarning($"Not Found Coupon with Id: {coupon.Id}");
                return NotFound();
            }

            _logger.LogInformation($"Success updating {coupon.ProductName} coupon | Id: {coupon.Id}");
            return Ok(updateResult);
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            var deleteResult = await _repository.DeleteDiscountAsync(productName);

            if (!deleteResult)
            {
                _logger.LogWarning($"Not Found Coupon with Name: {productName}");
                return NotFound();
            }

            _logger.LogInformation($"Success deleting {productName} coupon");
            return Ok(deleteResult);
        }
    }
}
