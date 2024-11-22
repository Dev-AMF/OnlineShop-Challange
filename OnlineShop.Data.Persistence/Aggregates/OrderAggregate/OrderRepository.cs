using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using OnlineShop.Persistence.Context.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context.Aggregates.OrderAggregate
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OnlineShopDbContext _context;

        public OrderRepository(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task<OrderEntity?> GetByIdAsync(Guid orderId)
        {
            return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task AddAsync(OrderEntity order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersByPersonAsync(Guid personId)
        {
            return await _context.Orders.Where(o => o.PersonId == personId).ToListAsync();
        }

        public async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
