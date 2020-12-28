using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.Autor.Models;
using TiendaServices.Api.Autor.Persistence;

namespace TiendaServices.Api.Autor.Application
{
    public class NewAutorCommand : IRequest
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }

    public class NewAutorCommandHandler : IRequestHandler<NewAutorCommand>
    {
        private readonly AutorContext _context;
        public NewAutorCommandHandler(AutorContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(NewAutorCommand request, CancellationToken cancellationToken)
        {
            var autorLibro = new AutorLibro
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                FechaNacimiento = request.FechaNacimiento,
                AutorLibroGuid = Guid.NewGuid().ToString()
            };

            await _context.AddAsync(autorLibro);
            var res = await _context.SaveChangesAsync();

            if(res > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el autor.");
        }
    }
}
