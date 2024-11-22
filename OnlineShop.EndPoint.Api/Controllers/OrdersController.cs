using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Application.Order.Commands;
using OnlineShop.Core.Application.Order.Queries;

namespace OnlineShop.EndPoint.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all orders.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves a specific order by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);
            if (order == null)
                return NotFound($"Order with ID {id} not found.");
            return Ok(order);
        }

        /// <summary>
        /// Retrieves all orders for a specific person.
        /// </summary>
        [HttpGet("person/{personId}")]
        public async Task<ActionResult> GetOrdersByPersonId(Guid personId)
        {
            var query = new GetOrdersByPersonQuery(personId);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
        }

        

     
    }

}
