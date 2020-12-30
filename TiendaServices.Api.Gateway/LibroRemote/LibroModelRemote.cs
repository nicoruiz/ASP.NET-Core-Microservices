using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServices.Api.Gateway.LibroRemote
{
    public class LibroModelRemote
    {
        public Guid? LibroMaterialId { get; set; }
        public string Titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public Guid? AutorLibro { get; set; }

        public AutorModelRemote AutorData { get; set; }
    }
}
