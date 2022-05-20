using AutoMapper;
using Ordering.Application.Features.Command.CheckoutOrder;
using Ordering.Application.Features.Command.DeleteOrder;
using Ordering.Application.Features.Command.UpdateOrder;
using Ordering.Application.Features.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
            CreateMap<Order, DeleteOrderCommand>().ReverseMap();
        }
    }
}
