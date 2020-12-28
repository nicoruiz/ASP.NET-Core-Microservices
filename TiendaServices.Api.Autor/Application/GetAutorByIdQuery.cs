using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.Autor.Models;
using TiendaServices.Api.Autor.Persistence;

namespace TiendaServices.Api.Autor.Application
{
    public class GetAutorByIdQuery : IRequest<AutorDto>
    {
        public string AutorGuid { get; set; }
    }

    public class GetAutorByIdQueryHandler : IRequestHandler<GetAutorByIdQuery, AutorDto>
    {
        private readonly AutorContext _context;
        private readonly IMapper _mapper;

        public GetAutorByIdQueryHandler(AutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AutorDto> Handle(GetAutorByIdQuery request, CancellationToken cancellationToken)
        {
            var autor = await _context
                .AutorLibros
                .Where(a => a.AutorLibroGuid == request.AutorGuid)
                .FirstOrDefaultAsync();

            if(autor == null)
            {
                throw new Exception("No se encontró el autor indicado.");
            }

            var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

            return autorDto;
        }
    }
}
