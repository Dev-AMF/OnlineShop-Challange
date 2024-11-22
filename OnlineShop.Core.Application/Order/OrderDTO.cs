using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Order
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingOption { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
