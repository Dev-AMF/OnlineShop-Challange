using MediatR;
using OnlineShop.Core.Application.Customer.Commands;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.Entities;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate.ValueObjects;
using OnlineShop.Persistence.Context.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Customer.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerName = CustomerName.Parse(request.FirstName, request.LastName);

            var customer = new CustomerEntity(Guid.NewGuid(),customerName);

            await _customerRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();
            return customer.Id;
        }
    }
}
