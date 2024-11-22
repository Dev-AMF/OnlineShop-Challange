using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; private set; }
        public CustomerName Name { get; private set; }

        public CustomerEntity(Guid id, CustomerName? name)
        {
            if(id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));


            Id = id;
            Name = CustomerName.Parse(name);
        }
        public void UpdateName(CustomerName? name) 
        {
            this.Name = CustomerName.Parse(name);
        }

        protected CustomerEntity()
        {
                
        }
    }
}
