using MediatR;
using OnlineShop.Core.Application.Customer.Commands;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Persistence.Context.UnitOfWork;

namespace OnlineShop.Core.Application.Customer.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand,Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {request.Id} not found.");

            var updatedName = CustomerName.Parse(request.FirstName, request.LastName);
            customer.UpdateName(updatedName); 
            await _customerRepository.UpdateAsync(customer);
            await _unitOfWork.CommitAsync();
            return customer.Id;
        }

       
    }
}
