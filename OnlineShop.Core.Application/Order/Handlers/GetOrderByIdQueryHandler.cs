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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with Id {request.OrderId} not found.");

            return new OrderDTO
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
            };
        }
    }
}
