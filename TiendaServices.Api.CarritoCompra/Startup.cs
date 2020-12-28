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
using TiendaServices.Api.CarritoCompra.Application;
using TiendaServices.Api.CarritoCompra.Persistence;
using TiendaServices.Api.CarritoCompra.RemoteInterfaces;
using TiendaServices.Api.CarritoCompra.RemoteServices;

namespace TiendaServices.Api.CarritoCompra
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
            services.AddScoped<ILibrosService, LibrosService>();
            services.AddControllers();
            services.AddDbContext<CarritoContext>(options => 
            {
                options.UseMySQL(Configuration.GetConnectionString("MySqlServer"));
            });
            services.AddMediatR(typeof(NewCarritoSesionCommandHandler).Assembly);
            services.AddHttpClient("LibrosAPI", config => 
            {
                config.BaseAddress = new Uri(Configuration.GetSection("Services:LibrosAPI").Value);
            });            
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
        }
    }
}
