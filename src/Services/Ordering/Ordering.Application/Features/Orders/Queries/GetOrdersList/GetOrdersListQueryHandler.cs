using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos;
using Ordering.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    //Applying CQRS Pattern with MediatR
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQueryRequest, IReadOnlyCollection<OrdersDto>> //MediatR 'Handler<Request, Response>' [GetOrdersListQuery is a MediatR class IRequest]
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrdersListQueryHandler> _logger;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrdersListQueryHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyCollection<OrdersDto>> Handle(GetOrdersListQueryRequest request, CancellationToken cancellationToken) //Handle is a method that should be implemented for MediatR interface
        {
            var orderList = await _orderRepository.GetOrdersByUserNameAsync(request.UserName);
            _logger.LogInformation($"Query '{nameof(GetOrdersListQueryRequest)}' performed successfully. Found {orderList.Count} orders.");
            return _mapper.Map<ReadOnlyCollection<OrdersDto>>(orderList);
        }
    }
}
