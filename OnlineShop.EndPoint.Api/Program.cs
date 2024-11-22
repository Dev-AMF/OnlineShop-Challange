
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Application.Order.Commands;
using OnlineShop.Core.Domain;
using OnlineShop.Persistence.Context;
using OnlineShop.Persistence.Context.Context;
using OnlineShop.Persistence.Context.DataSeed;
using System.Reflection;

namespace OnlineShop.EndPoint.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
                config.RegisterServicesFromAssembly(typeof(Persistence_ServiceCollections).Assembly);
            });
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddDomainServices();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // **Register DataSeeder as Transient**
            builder.Services.AddTransient<DataSeeder>();

            var app = builder.Build();


            //// Apply migrations and seed data
            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<OnlineShopDbContext>();
            //    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

            //    // Apply migrations
            //    dbContext.Database.Migrate();

            //    // Seed data
            //   //  seeder.SeedAsync().GetAwaiter().GetResult();
            //}



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
