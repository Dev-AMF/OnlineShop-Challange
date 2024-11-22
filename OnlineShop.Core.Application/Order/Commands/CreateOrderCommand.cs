using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Order.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid PersonId { get; set; }
        public string? DiscountType { get; set; }
        public decimal? DiscountAmount { get; set; }
        public List<CreateOrderItemCommand> OrderItems { get; set; }= new List<CreateOrderItemCommand>();

        public CreateOrderCommand(Guid personId, List<CreateOrderItemCommand> orderItems, string? discountType, decimal? discountAmount)
        {
            PersonId = personId;
            OrderItems = orderItems;
            DiscountType = discountType;
            DiscountAmount = discountAmount;
        }
    }

    public class CreateOrderItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PackagingMethod { get; set; }
        public int Quantity { get; set; }
    }
}
