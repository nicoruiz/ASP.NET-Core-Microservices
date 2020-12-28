using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServices.Api.CarritoCompra.RemoteInterfaces;
using TiendaServices.Api.CarritoCompra.RemoteModels;

namespace TiendaServices.Api.CarritoCompra.RemoteServices
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ILibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClientFactory, ILogger<ILibrosService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool result, LibroRemote libro, string errorMessage)> GetLibro(Guid libroId)
        {
            try
            {
                var librosClient = _httpClientFactory.CreateClient("LibrosAPI");
                var response = await librosClient.GetAsync($"api/libros/{libroId}");
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<LibroRemote>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
