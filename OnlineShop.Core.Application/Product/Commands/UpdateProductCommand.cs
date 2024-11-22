using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Product.Commands
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string PackagingMethod { get; set; }
        public decimal Price { get; set; }

        public UpdateProductCommand(Guid productId, string name, string packagingMethod, decimal price)
        {
            ProductId = productId;
            Name = name;
            PackagingMethod = packagingMethod;
            Price = price;
        }
    }
}
