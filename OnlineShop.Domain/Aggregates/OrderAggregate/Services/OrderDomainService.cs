using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        private readonly decimal _minimumFinalPrice = 50000m;
        private readonly decimal _minimumorderItem = 1;
        private readonly OrderingHours _validOrderingHours = new OrderingHours(TimeSpan.FromHours(8), TimeSpan.FromHours(19));


        public bool ValidateOrderingHour(OrderEntity order)
        {
            

            if (!_validOrderingHours.IsWithinRange(order.OrderDate.TimeOfDay))
                throw new InvalidOperationException("Orders can only be placed between 8:00 AM and 7:00 PM.");


            return true;
        }
        public bool ValidateOrderItems(OrderEntity order)
        {
            if(order.OrderItems.Count < _minimumorderItem)
                throw new InvalidOperationException($"At least {_minimumorderItem} is necessary for placing an order.");

            return true;
        }
        public bool ValidateTotalPrice(OrderEntity order)
        {
        
            if (order.CalculateTotalPrice().Value < _minimumFinalPrice)
                throw new InvalidOperationException($"Order's total price must be at least {_minimumFinalPrice} Tomans.");

            return true;
        }


    }
}
