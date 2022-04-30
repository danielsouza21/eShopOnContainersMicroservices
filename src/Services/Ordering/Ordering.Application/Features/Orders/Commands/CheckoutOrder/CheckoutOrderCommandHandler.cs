using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Interfaces.ExternalServices;
using Ordering.Application.Interfaces.Repositories;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommandRequest, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IEmailService emailService, IMapper mapper, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CheckoutOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrderAdded = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrderAdded.Id} is successfully created.");

            await SendMail(newOrderAdded);
            return newOrderAdded.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new SendEmailRequest() { 
                To = "danielloko1999@outlook.com", 
                Body = $"Order for {order.UserName} was created.", 
                Subject = $"Order with full price of {order.TotalPrice} was created" 
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
