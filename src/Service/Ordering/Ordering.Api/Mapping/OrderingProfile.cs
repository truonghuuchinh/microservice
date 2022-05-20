using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Command.CheckoutOrder;

namespace Ordering.Api.Mapping
{
    public class OrderingProfile:Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
