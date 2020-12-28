using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.Libro.Models;

namespace TiendaServices.Api.Libro.Persistence
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext() { }

        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options) { }

        public virtual DbSet<LibroMaterial> Libros { get; set; }
    }
}
