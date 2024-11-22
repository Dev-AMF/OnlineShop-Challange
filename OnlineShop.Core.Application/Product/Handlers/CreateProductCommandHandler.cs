using MediatR;
using OnlineShop.Core.Application.Product.Commands;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Persistence.Context.UnitOfWork;

namespace OnlineShop.Core.Application.Product.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity(
                Guid.NewGuid(),
                ProductName.Parse(request.Name),
                ProductPackingMethod.Parse(Enum.Parse<PackingMethods>(request.PackagingMethod)),
                ProductPrice.Parse(request.Price)
            );

            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return product.Id;
        }
    }
}
