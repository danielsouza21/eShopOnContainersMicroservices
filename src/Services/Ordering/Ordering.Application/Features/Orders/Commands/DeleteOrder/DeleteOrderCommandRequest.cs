using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
}
