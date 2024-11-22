using MediatR;
using OnlineShop.Core.Application.Order.Commands;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Services;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using OnlineShop.Persistence.Context.Aggregates.ProductAggregate;
using OnlineShop.Persistence.Context.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Order.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderDomainService _orderDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IProductRepository productRepository, IOrderDomainService orderDomainService)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderDomainService = orderDomainService;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
           
            var person = await _customerRepository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new Exception("Person not found.");

            var order = new OrderEntity(Guid.NewGuid(), request.PersonId, DateTime.Now);

            if (request.DiscountType != null & request.DiscountAmount != null)
            {
                var orderDiscount = Discount.Parse(request.DiscountType, request.DiscountAmount);
                order.AddDiscount(orderDiscount);
            }

            foreach (var item in request.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (item == null)
                    throw new Exception($"Product with ID {item.ProductId} not found.");

                var orderItem = new OrderItemEntity(
                    Guid.NewGuid(),
                    item.ProductId,
                    ProductName.Parse(item.ProductName),
                    ProductPrice.Parse(item.UnitPrice),
                    ProductPackingMethod.Parse(Enum.Parse<PackingMethods>(item.PackagingMethod)),
                    Quantity.Parse(item.Quantity)
                );
                order.AddItem(orderItem.Id, item.ProductId,orderItem.Name, orderItem.UnitPrice, orderItem.PackagingMethod, orderItem.Quantity);
            }

            // Validate Order
            _orderDomainService.ValidateOrderItems(order);
            _orderDomainService.ValidateOrderingHour(order);
            _orderDomainService.ValidateTotalPrice(order);

            // Save Order
            await _orderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return order.Id;
        }
    }
}
