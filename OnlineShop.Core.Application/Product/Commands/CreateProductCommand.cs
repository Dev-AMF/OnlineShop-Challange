using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Product.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string PackagingMethod { get; set; }
        public decimal Price { get; set; }

        public CreateProductCommand(string name, string packagingMethod, decimal price)
        {
            Name = name;
            PackagingMethod = packagingMethod;
            Price = price;
        }
    }
}
