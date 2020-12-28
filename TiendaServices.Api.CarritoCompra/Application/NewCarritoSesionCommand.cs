using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.CarritoCompra.Models;
using TiendaServices.Api.CarritoCompra.Persistence;

namespace TiendaServices.Api.CarritoCompra.Application
{
    public class NewCarritoSesionCommand : IRequest
    {
        public DateTime FechaCreacionSesion { get; set; }

        public List<string> ProductosId { get; set; }
    }

    public class NewCarritoSesionCommandHandler : IRequestHandler<NewCarritoSesionCommand>
    {
        public readonly CarritoContext _context;

        public NewCarritoSesionCommandHandler(CarritoContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(NewCarritoSesionCommand request, CancellationToken cancellationToken)
        {
            var carritoSesion = new CarritoSesion
            {
                FechaCreacion = request.FechaCreacionSesion
            };

            await _context.AddAsync(carritoSesion);
            var value = await _context.SaveChangesAsync();

            if (value == 0)
            {
                throw new Exception("Error al intentar guardar el carrito de comprar.");
            }

            int id = carritoSesion.CarritoSesionId;

            foreach(var prod in request.ProductosId)
            {
                var carritoSesionDetalle = new CarritoSesionDetalle
                {
                    CarritoSesionId = id,
                    FechaCreacion = DateTime.Now,
                    ProductoSeleccionadoId = prod
                };

                await _context.CarritoSesionDetalles.AddAsync(carritoSesionDetalle);
            }

            value = await _context.SaveChangesAsync();

            if(value > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo guardar el detalle del carrito de compras.");
        }
    }
}
