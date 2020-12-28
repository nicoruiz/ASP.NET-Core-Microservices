using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.Libro.Models;
using TiendaServices.Api.Libro.Persistence;

namespace TiendaServices.Api.Libro.Application
{
    public class NewLibroCommand : IRequest
    {
        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public Guid? AutorLibro { get; set; }
    }

    public class NewLibroCommandHandler : IRequestHandler<NewLibroCommand>
    {
        private readonly LibreriaContext _context;
        public NewLibroCommandHandler(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(NewLibroCommand request, CancellationToken cancellationToken)
        {
            var libro = new LibroMaterial
            {
                Titulo = request.Titulo,
                FechaPublicacion = request.FechaPublicacion,
                AutorLibro = request.AutorLibro
            };

            var entity = await _context.Libros.AddAsync(libro);
            var res = await _context.SaveChangesAsync();

            if (res > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el libro.");
        }
    }
}
