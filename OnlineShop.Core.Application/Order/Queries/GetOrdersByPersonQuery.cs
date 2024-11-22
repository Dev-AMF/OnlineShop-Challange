using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Order.Queries
{
    public class GetOrdersByPersonQuery : IRequest<IEnumerable<OrderDTO>>
    {
        public Guid PersonId { get; set; }

        public GetOrdersByPersonQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
