using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Domain
{
    public static class Domain_ServiceCollections
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IOrderDomainService, OrderDomainService>();

            return services;
        }
    }
}
