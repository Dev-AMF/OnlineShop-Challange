using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Application.Customer.Commands;
using OnlineShop.Core.Application.Customer.Queries;
using OnlineShop.Core.Application.Customer;

namespace OnlineShop.EndPoint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(Guid id)
        {
            var query = new GetCustomerByIdQuery(id);
            var customer = await _mediator.Send(query);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");
            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customerId);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            var updatedCustomerId = await _mediator.Send(command);
            return Ok(updatedCustomerId);
        }

        /// <summary>
        /// Deletes a customer by their unique identifier.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand(id);
            var deletedCustomerId = await _mediator.Send(command);
            return Ok(deletedCustomerId);
        }
    }
}
