using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.Autor.Application;
using TiendaServices.Api.Autor.Persistence;
using TiendaServices.Api.Autor.RabbitHandler;
using TiendaServices.RabbitMQ.Bus.EventQueue;
using TiendaServices.RabbitMQ.Bus.Implement;
using TiendaServices.RabbitMQ.Bus.RabbitBus;

namespace TiendaServices.Api.Autor
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
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<NewAutorCommand>());

            services.AddDbContext<AutorContext>(options => 
            {
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL"));
            });

            services.AddMediatR(typeof(NewAutorCommandHandler).Assembly);
            services.AddAutoMapper(typeof(GetAutorQuery));
            services.AddTransient<IEventHandler<EmailEventQueue>, EmailEventHandler>();
            services.AddTransient<IRabbitEventBus, RabbitEventBus>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
            eventBus.Subscribe<EmailEventQueue, EmailEventHandler>();
        }
    }
}
