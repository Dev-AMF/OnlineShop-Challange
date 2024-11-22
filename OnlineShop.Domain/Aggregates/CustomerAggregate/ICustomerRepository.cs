using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.CustomerAggregate
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity?> GetByIdAsync(Guid customerId);
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task AddAsync(CustomerEntity item);
        Task UpdateAsync(CustomerEntity item);
        void Remove(Guid id);
    }
}
