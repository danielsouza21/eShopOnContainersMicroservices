using AutoMapper;
using Basket.Domain.Entities;
using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Services.GrpcService;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly IMapper _mapper;
        private readonly ILogger<BasketController> _logger;

        public BasketController(
            IBasketRepository repository, 
            DiscountGrpcService discountGrpcService,
            IPublishEndpoint publishEndpoint,
            IMapper mapper,
            ILogger<BasketController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //Get existing basket with total price
            var basket = await _repository.GetBasketForUserNameAsync(basketCheckout.UserName);

            if (basket == null) return BadRequest();

            //Set TotalPrice on basketCheckout eventMessage and send checkout event to rabbitmq
            var basketEventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            basketEventMessage.TotalPrice = basket.TotalPrice;

            //Publish an event that will be processed by 'BasketCheckoutConsumer.cs' in OrderingAPI
            //Fanout Exchange: message to all bound queues indiscriminately
            await _publishEndpoint.Publish(basketEventMessage);

            //Remove the basket
            await _repository.DeleteBasketAsync(basket.UserName);

            return Accepted();
        }
    }
}
