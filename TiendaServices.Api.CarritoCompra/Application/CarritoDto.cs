using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServices.Api.CarritoCompra.Application
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }

        public DateTime? FechaCreacionSesion { get; set; }

        public List<CarritoDetalleDto> CarritoDetalles { get; set; }
    }
}
