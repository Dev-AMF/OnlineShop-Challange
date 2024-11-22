using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Domain.Aggregates.CustomerAggregate;
using OnlineShop.Core.Domain.Aggregates.OrderAggregate;
using OnlineShop.Core.Domain.Aggregates.ProductAggregate;
using OnlineShop.Persistence.Context.Aggregates.CustomerAggregate;
using OnlineShop.Persistence.Context.Aggregates.OrderAggregate;
using OnlineShop.Persistence.Context.Aggregates.ProductAggregate;
using OnlineShop.Persistence.Context.Context;
using OnlineShop.Persistence.Context.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Persistence.Context
{
    public static class Persistence_ServiceCollections
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OnlineShopDbDefaultConn");


            services.AddDbContext<OnlineShopDbContext>
                (options =>
                    options.UseSqlServer(connectionString)
                );

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();


            services.AddScoped<IUnitOfWork, Persistence.Context.UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
