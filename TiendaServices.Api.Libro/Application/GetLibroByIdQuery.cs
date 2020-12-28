using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.Libro.Models;
using TiendaServices.Api.Libro.Persistence;

namespace TiendaServices.Api.Libro.Application
{
    public class GetLibroByIdQuery : IRequest<LibroDto>
    {
        public Guid? LibroId { get; set; }
    }

    public class GetLibroByIdQueryHandler : IRequestHandler<GetLibroByIdQuery, LibroDto>
    {
        private readonly LibreriaContext _context;
        private readonly IMapper _mapper;

        public GetLibroByIdQueryHandler(LibreriaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LibroDto> Handle(GetLibroByIdQuery request, CancellationToken cancellationToken)
        {
            var libro = await _context
                .Libros
                .Where(a => a.LibroMaterialId == request.LibroId)
                .FirstOrDefaultAsync();

            if (libro == null)
            {
                throw new Exception("No se encontró el libro indicado.");
            }

            var libroDto = _mapper.Map<LibroMaterial, LibroDto>(libro);

            return libroDto;
        }
    }
}
