using OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.Entitites
{
    public class OrderEntity
    {
        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public ShippingOption? ShippingOption { get; private set; }
        public Discount? Discount { get; private set; }

        public OrderPrice? TotalPrice { get; private set; }

        private readonly List<OrderItemEntity> _orderItems;
        public IReadOnlyCollection<OrderItemEntity> OrderItems => _orderItems.AsReadOnly();

        protected OrderEntity() { }
        public OrderEntity(Guid id, Guid personId, DateTime orderDate, Discount? discount = null)
        {
            if(id == null) throw new ArgumentNullException("id");
            if(personId == null) throw new ArgumentNullException("personId");
            if(id.Equals(Guid.Empty)) throw new ArgumentException("Id cannot be empty.");
            if(personId.Equals(Guid.Empty)) throw new ArgumentException("PersonId cannot be empty.");
            if (orderDate == default) throw new ArgumentException("OrderDate must be a valid date.");

            Id = id;
            PersonId = personId;
            OrderDate = orderDate;
            _orderItems = new List<OrderItemEntity>();
            ShippingOption = ShippingOption.Parse(ShippingOptions.RegularPost.ToString());
            Discount = discount ?? Discount.Parse(DiscountType.Amount.ToString(), 0);
            TotalPrice = CalculateTotalPrice();
        }

        private OrderPrice ApplyDiscount(Discount discount,OrderPrice orderPrice)
        {
            decimal discountedOrderPrice = orderPrice.Value;
            decimal notDiscountedOrderPrice = orderPrice.Value;

            if (discount.DiscountType == DiscountType.Percentage)
            {
                discountedOrderPrice =  orderPrice.Value * (1 - discount.DiscountAmount / 100);
            }
            if (discount.DiscountType == DiscountType.Amount) 
            {
                discountedOrderPrice = orderPrice.Value - discount.DiscountAmount;
            }
            if(discountedOrderPrice <= 0)
                 throw new ArgumentException("discount can not be greater than total price!");

            return OrderPrice.Parse(discountedOrderPrice);
        }
        private void UpdateShippingOption()
        {
            if (_orderItems.Any(item => item.PackagingMethod.Equals(PackingMethods.Fragile)))
            {
                ShippingOption = ShippingOption.Parse(ShippingOptions.ExpressPost.ToString());
            }
            else
            {
                ShippingOption = ShippingOption.Parse(ShippingOptions.RegularPost.ToString());
            }
        }
        public void AddItem(Guid itemId, Guid productId,ProductName itemName, ProductPrice unitPrice, ProductPackingMethod packagingMethod, Quantity quantity)
        {
            var orderItem = new OrderItemEntity(itemId, productId, itemName, unitPrice, packagingMethod, quantity);
            _orderItems.Add(orderItem);

            UpdateShippingOption();
            TotalPrice = CalculateTotalPrice();
        }
        public void RemoveItem(Guid itemId)
        {
            var orderItem = _orderItems.SingleOrDefault(item => item.Id == itemId);

            if (orderItem == null)
            {
                throw new ArgumentException($"Order item with ID {itemId} not found.");
            }

            _orderItems.Remove(orderItem);

            UpdateShippingOption();
            TotalPrice = CalculateTotalPrice();
        }
        public OrderPrice CalculateTotalPrice()
        {
        
            var totalPrice =  OrderPrice.Parse(_orderItems.Sum(item => item.UnitPrice.Value * Convert.ToDecimal(item.Quantity.Value)));

            if (Discount != null & totalPrice.Value > 0)
            {
                if (Discount.DiscountAmount > 0)
                {

                    totalPrice = ApplyDiscount(Discount, totalPrice);
                }
             
            }

            return totalPrice;
        }
        public void AddDiscount(Discount discount)
        {
            var discounttemp = Discount.Parse(discount);
            Discount = discounttemp;

            CalculateTotalPrice();


        }
    }
}
