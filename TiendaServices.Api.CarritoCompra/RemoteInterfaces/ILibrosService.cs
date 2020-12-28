using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.CarritoCompra.RemoteModels;

namespace TiendaServices.Api.CarritoCompra.RemoteInterfaces
{
    public interface ILibrosService
    {
        Task<(bool result, LibroRemote libro, string errorMessage)> GetLibro(Guid LibroId);
    }
}
