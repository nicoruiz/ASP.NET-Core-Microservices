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
    public class GetLibroQuery : IRequest<List<LibroDto>> { }

    public class GetLibroQueryHandler : IRequestHandler<GetLibroQuery, List<LibroDto>>
    {
        private readonly LibreriaContext _context;
        private readonly IMapper _mapper;

        public GetLibroQueryHandler(LibreriaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LibroDto>> Handle(GetLibroQuery request, CancellationToken cancellationToken)
        {
            var libros = await _context.Libros.ToListAsync();

            var librosDto = _mapper.Map<List<LibroMaterial>, List<LibroDto>>(libros);

            return librosDto;
        }
    }
}
