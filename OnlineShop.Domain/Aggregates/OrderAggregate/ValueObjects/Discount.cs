using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain.Aggregates.OrderAggregate.ValueObjects
{
    public enum DiscountType
    {
        Percentage,
        Amount
    }
    public class Discount
    {
        public DiscountType DiscountType { get; }
        public decimal DiscountAmount { get; }

        protected Discount(DiscountType discountType, decimal discountAmount)
        {

            if (discountType == DiscountType.Percentage)
            {
                if (discountAmount <= 0 || discountAmount > 100)
                    throw new ArgumentException("Discount percentage must be between 0 and 100.", nameof(discountAmount));
            }
            if (discountType == DiscountType.Amount)
            {
                if (discountAmount < 0)
                    throw new ArgumentException("Discount amount must be positive.", nameof(discountAmount));
            }
            DiscountType = discountType;
            DiscountAmount = discountAmount;
        }

        public static Discount Parse(string? type, decimal? amount)
        {
            var _percentage = DiscountType.Percentage.ToString();
            var _amount = DiscountType.Amount.ToString();  

            if (type == null) throw new ArgumentException($"DiscountType can not be empty.", nameof(type));
            if (amount == null) throw new ArgumentException($"DiscountAmount can not be empty.", nameof(amount));

            if (type != _percentage & type != _amount)
                throw new ArgumentException($"Only {_percentage} and {_amount} are valid values for DiscountType.", nameof(type));


            return type == _percentage
                ? new Discount(DiscountType.Percentage, Convert.ToDecimal(amount))
                : new Discount(DiscountType.Amount, Convert.ToDecimal(amount));
        }
        public static Discount Parse(Discount discount)
        {
            if(discount == null) throw new ArgumentNullException(nameof(discount));

            return new Discount(discount.DiscountType,discount.DiscountAmount);
        }

    }
}
