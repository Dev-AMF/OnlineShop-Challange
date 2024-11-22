using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using OnlineShop.Persistence.Context.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context.Aggregates.CustomerAggregate
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OnlineShopDbContext _context;

        public CustomerRepository(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerEntity?> GetByIdAsync(Guid customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddAsync(CustomerEntity item)
        {
            await _context.Customers.AddAsync(item);
        }

        public async Task UpdateAsync(CustomerEntity item)
        {
            await Task.Run(() =>
            {
                _context.Customers.Update(item);
            });
        }

        public void Remove(Guid id)
        {
            var entity = _context.Customers.Find(id);
            if (entity != null)
            {
                _context.Customers.Remove(entity);
            }
        }
    }
    
}
