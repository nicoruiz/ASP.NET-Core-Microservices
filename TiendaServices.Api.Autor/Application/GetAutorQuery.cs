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
    public class GetAutorQuery : IRequest<List<AutorDto>> { }

    public class GetAutorQueryHandler : IRequestHandler<GetAutorQuery, List<AutorDto>>
    {
        private readonly AutorContext _context;
        private readonly IMapper _mapper;
        public GetAutorQueryHandler(AutorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AutorDto>> Handle(GetAutorQuery request, CancellationToken cancellationToken)
        {
            var autores = await _context.AutorLibros.ToListAsync();
            var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);

            return autoresDto;
        }
    }
}
