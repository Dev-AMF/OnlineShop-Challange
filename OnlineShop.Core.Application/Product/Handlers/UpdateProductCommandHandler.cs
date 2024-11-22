using MediatR;
using OnlineShop.Core.Application.Product.Commands;
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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async  Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with Id {request.ProductId} not found.");

            product.UpdateName(ProductName.Parse(request.Name)); 
            product.UpdatePackagingMethod(ProductPackingMethod.Parse(Enum.Parse<PackingMethods>(request.PackagingMethod))); 
            product.UpdatePrice(ProductPrice.Parse(request.Price)); 

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            return product.Id;
        }

        
    }
}
