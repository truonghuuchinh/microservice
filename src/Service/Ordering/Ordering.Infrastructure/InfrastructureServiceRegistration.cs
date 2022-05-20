
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Models;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<OrderContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            service.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            service.AddScoped<IOrderRepository, OrderRepository>();

            service.Configure<EmailSettings>(c=>
            {
                c.ApiKey = configuration.GetSection("EmailSettings:ApiKey").Value;
                c.FromAddress = configuration.GetSection("EmailSettings:FromAddress").Value;
                c.FromName = configuration.GetSection("EmailSettings:FromName").Value;
            });
            service.AddTransient<IEmailService, EmailService>();

            return service;
        }
    }
}
