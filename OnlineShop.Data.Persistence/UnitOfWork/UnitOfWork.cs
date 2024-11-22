using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using OnlineShop.Persistence.Context.Aggregates.CustomerAggregate;
using OnlineShop.Persistence.Context.Aggregates.OrderAggregate;
using OnlineShop.Persistence.Context.Aggregates.ProductAggregate;
using OnlineShop.Persistence.Context.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext _context;

        public IOrderRepository Orders { get; }
        public IProductRepository Products { get; }
        public ICustomerRepository Customers { get; }

        public UnitOfWork(OnlineShopDbContext context)
        {
            _context = context;
            Orders = new OrderRepository(_context);
            Products = new ProductRepository(_context);
            Customers = new CustomerRepository(_context);
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Optionally, rethrow the exception to propagate it
                throw new InvalidOperationException("An error occurred while saving changes to the database.", ex);
            }
            finally
            {
                Dispose();
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
