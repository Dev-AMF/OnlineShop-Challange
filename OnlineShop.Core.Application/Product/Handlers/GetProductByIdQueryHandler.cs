using MediatR;
using OnlineShop.Core.Application.Product.Queries;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Product.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with Id {request.ProductId} not found.");

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name.Value,
                PackagingMethod = product.PackagingMethod.Value.ToString(),
                Price = product.Price.Value
            };
        }
    }
}
