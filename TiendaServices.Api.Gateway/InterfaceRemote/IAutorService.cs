using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.Gateway.LibroRemote;

namespace TiendaServices.Api.Gateway.InterfaceRemote
{
    public interface IAutorService
    {
        Task<(bool result, AutorModelRemote autor, string errorMessage)> GetAutor(Guid autorId);
    }
}
