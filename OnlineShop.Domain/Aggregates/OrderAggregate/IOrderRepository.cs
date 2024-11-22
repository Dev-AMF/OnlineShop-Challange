using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository
    {
        Task<OrderEntity?> GetByIdAsync(Guid orderId);
        Task AddAsync(OrderEntity order);
        Task<IEnumerable<OrderEntity>> GetOrdersByPersonAsync(Guid personId);

        Task<IEnumerable<OrderEntity>> GetAllAsync();
    }
}
