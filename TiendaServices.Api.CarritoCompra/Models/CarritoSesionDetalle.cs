using System;

namespace TiendaServices.Api.CarritoCompra.Models
{
    public class CarritoSesionDetalle
    {
        public int CarritoSesionDetalleId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string ProductoSeleccionadoId { get; set; }

        public int CarritoSesionId { get; set; }

        public CarritoSesion CarritoSesion { get; set; }
    }
}
