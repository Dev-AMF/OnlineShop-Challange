using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Application.Customer.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
    }
}
