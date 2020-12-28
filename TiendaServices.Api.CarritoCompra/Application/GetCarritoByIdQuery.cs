using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.CarritoCompra.Persistence;
using TiendaServices.Api.CarritoCompra.RemoteInterfaces;

namespace TiendaServices.Api.CarritoCompra.Application
{
    public class GetCarritoByIdQuery : IRequest<CarritoDto>
    {
        public int CarritoSesionId { get; set; }
    }

    public class GetCarritoByIdQueryHandler : IRequestHandler<GetCarritoByIdQuery, CarritoDto>
    {
        private readonly CarritoContext _context;
        private readonly ILibrosService _librosService;

        public GetCarritoByIdQueryHandler(CarritoContext context, ILibrosService librosService)
        {
            _context = context;
            _librosService = librosService;
        }

        public async Task<CarritoDto> Handle(GetCarritoByIdQuery request, CancellationToken cancellationToken)
        {
            var carritoSesion = await _context.CarritoSesions.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
            var carritoSesionDetalle = await _context.CarritoSesionDetalles.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

            var carritoDetalleDto = new List<CarritoDetalleDto>();

            foreach(var detalle in carritoSesionDetalle)
            {
                var response = await _librosService.GetLibro(new Guid(detalle.ProductoSeleccionadoId));
                if(response.result)
                {
                    var objLibro = response.libro;
                    carritoDetalleDto.Add(new CarritoDetalleDto {
                        LibroId = objLibro.LibroMaterialId,
                        TituloLibro = objLibro.Titulo,
                        AutorLibro = objLibro.AutorLibro.ToString(),
                        FechaPublicacion = objLibro.FechaPublicacion
                    });
                }
            }

            var carritoDto = new CarritoDto
            {
                CarritoId = carritoSesion.CarritoSesionId,
                FechaCreacionSesion = carritoSesion.FechaCreacion,
                CarritoDetalles = carritoDetalleDto
            };

            return carritoDto;
        }
    }
}
