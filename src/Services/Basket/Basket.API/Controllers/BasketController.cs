using Basket.Domain.Entities;
using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService, ILogger<BasketController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasketForUserNameAsync(userName);

            var messageLog = basket is null ? $"User {userName} does not have a basket yet" : $"Success retrieving {userName} basket";
            _logger.LogInformation(messageLog);

            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //Getting all coupons from discount PostgreSQL for each basketItem using gRPC communication to DiscountService call
            //Communication as client configured before in project (.csproj) [Protobuf] and Startup.cs
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountForProductNameAsync(item.ProductName);
                item.Price -= coupon.Amount;
                _logger.LogInformation($"Found a discount of '{coupon.Amount}' for product '{item.ProductName}' when updating basket for '{basket.UserName}' user");
            }

            var basketUpdated = await _repository.UpsertBasketAsync(basket);

            _logger.LogInformation($"Success updating {basket.UserName} basket");
            return Ok(basketUpdated);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasketAsync(userName);
            _logger.LogInformation($"Success deleting {userName} basket");
            return Ok();
        }
    }
}
