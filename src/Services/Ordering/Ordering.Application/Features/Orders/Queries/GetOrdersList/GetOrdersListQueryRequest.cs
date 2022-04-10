using MediatR;
using Ordering.Application.Dtos;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryRequest : IRequest<IReadOnlyCollection<OrdersDto>> //IRequest from 'MediatR' for Queries (CQRS Pattern)
    {
        public string UserName { get; set; } //Unique parameter, required for the request

        public GetOrdersListQueryRequest(string userName)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        }
    }
}
