using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServices.Api.Gateway.InterfaceRemote;
using TiendaServices.Api.Gateway.LibroRemote;

namespace TiendaServices.Api.Gateway.ImplementRemote
{
    public class AutorService : IAutorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IAutorService> _logger;

        public AutorService(IHttpClientFactory httpClientFactory, ILogger<IAutorService> logger) 
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool result, AutorModelRemote autor, string errorMessage)> GetAutor(Guid autorId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AutorService");
                var response = await client.GetAsync($"/autores/{autorId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var autorResult = JsonSerializer.Deserialize<AutorModelRemote>(content, options);

                    return (true, autorResult, null);
                }
                else return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
