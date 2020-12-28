using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.CarritoCompra.Models;

namespace TiendaServices.Api.CarritoCompra.Persistence
{
    public class CarritoContext : DbContext
    {
        public CarritoContext(DbContextOptions<CarritoContext> options) : base(options) { }

        public DbSet<CarritoSesion> CarritoSesions { get; set; }

        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
    }
}
