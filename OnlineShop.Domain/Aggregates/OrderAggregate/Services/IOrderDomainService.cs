using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.Services
{
    public interface IOrderDomainService
    {
        bool ValidateOrderingHour(OrderEntity order);
        bool ValidateTotalPrice(OrderEntity order);
        bool ValidateOrderItems(OrderEntity order);
    }
}
