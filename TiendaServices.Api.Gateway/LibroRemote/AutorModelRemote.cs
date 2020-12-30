using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServices.Api.Gateway.LibroRemote
{
    public class AutorModelRemote
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string AutorLibroGuid { get; set; }
    }
}
