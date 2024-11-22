using MediatR;
using OnlineShop.Core.Application.Product.Commands;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using OnlineShop.Persistence.Context.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Product.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"product with Id {request.ProductId} not found.");

            var productId = product.Id;
            _productRepository.Remove(request.ProductId);
            await _unitOfWork.CommitAsync();

            return productId;
        }
    }
}
