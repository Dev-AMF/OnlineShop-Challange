using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository
    {
        Task<ProductEntity?> GetByIdAsync(Guid productId);
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task AddAsync(ProductEntity item);
        Task UpdateAsync(ProductEntity item);
        void Remove(Guid id);
    }
}
