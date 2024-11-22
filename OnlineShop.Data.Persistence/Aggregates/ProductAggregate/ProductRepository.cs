using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;
using OnlineShop.Persistence.Context.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context.Aggregates.ProductAggregate
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopDbContext _context;

        public ProductRepository(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity?> GetByIdAsync(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(ProductEntity item)
        {
            await _context.Products.AddAsync(item);
        }

        public async Task UpdateAsync(ProductEntity item)
        {
            await Task.Run(() =>
            {
                _context.Products.Update(item);
            });
           
        }

        public void Remove(Guid id)
        {
            var entity = _context.Products.Find(id);
            if (entity != null)
            {
                _context.Products.Remove(entity);
            }
        }
    }
}
