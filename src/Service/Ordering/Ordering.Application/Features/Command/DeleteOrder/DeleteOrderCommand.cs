using MediatR;

namespace Ordering.Application.Features.Command.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
