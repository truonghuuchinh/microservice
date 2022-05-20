using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ordering.Api.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add MassTransit - RabbitMQ configuration 
            services.AddMassTransit(opt =>
            {
                opt.AddConsumer<BasketCheckoutConsumer>();
                opt.UsingRabbitMq((context, config) =>
                {
                    config.Host(Configuration["EventBusSettings:HostAddress"]);
                    config.ReceiveEndpoint(EventBusConstants.BasketChecoutQueue, c =>
                    {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            //General configuration
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<BasketCheckoutConsumer>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
