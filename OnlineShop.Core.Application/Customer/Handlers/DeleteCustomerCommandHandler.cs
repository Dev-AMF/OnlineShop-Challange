using MediatR;
using OnlineShop.Core.Application.Customer.Commands;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using OnlineShop.Persistence.Context.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Customer.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand,Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {request.Id} not found.");

            var customerId = customer.Id;

            _customerRepository.Remove(customerId);
            await _unitOfWork.CommitAsync();
            
            return customerId;
        }
    }
}
