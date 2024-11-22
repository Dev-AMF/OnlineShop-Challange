using MediatR;
using OnlineShop.Core.Application.Order.Queries;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Order.Handlers
{
    public class GetOrdersByPersonQueryHandler : IRequestHandler<GetOrdersByPersonQuery, IEnumerable<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByPersonQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersByPersonQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByPersonAsync(request.PersonId);
            return orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                PersonId = order.PersonId,
                OrderDate = order.OrderDate,
                ShippingOption = order.ShippingOption.Value.ToString(),
                DiscountType = order.Discount?.DiscountType.ToString(),
                DiscountAmount = order.Discount?.DiscountAmount ?? 0,
                TotalPrice = order.TotalPrice.Value,
                OrderItems = order.OrderItems.Select(i => new OrderItemDTO
                {
                    Id = i.Id,
                    Name = i.Name.Value,
                    UnitPrice = i.UnitPrice.Value,
                    PackagingMethod = i.PackagingMethod.Value.ToString(),
                    Quantity = i.Quantity.Value
                }).ToList()
            });
        }
    }
}
