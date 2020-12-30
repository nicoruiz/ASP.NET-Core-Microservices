using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiendaServices.Api.Gateway.InterfaceRemote;
using TiendaServices.Api.Gateway.LibroRemote;

namespace TiendaServices.Api.Gateway.MessageHandler
{
    public class LibroHandler : DelegatingHandler
    {
        private readonly ILogger<LibroHandler> _logger;
        private readonly IAutorService _autorService;

        public LibroHandler(ILogger<LibroHandler> logger, IAutorService autorService)
        {
            _logger = logger;
            _autorService = autorService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var elapsedTime = Stopwatch.StartNew();
            _logger.LogInformation("Request started");
            var response = await base.SendAsync(request, cancellationToken);
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var libroResult = JsonSerializer.Deserialize<LibroModelRemote>(content, options);
                var autorResponse = await _autorService.GetAutor(libroResult.AutorLibro?? Guid.Empty);
                if (autorResponse.result)
                {
                    var autorObj = autorResponse.autor;
                    libroResult.AutorData = autorObj;
                    var resultStr = JsonSerializer.Serialize(libroResult);

                    response.Content = new StringContent(resultStr, System.Text.Encoding.UTF8, "application/json");
                }
            }

            _logger.LogInformation($"Process elapsed time: {elapsedTime.ElapsedMilliseconds}ms");

            return response;
        }
    }
}
