using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.CarritoCompra.Application;

namespace TiendaServices.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarritosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            return await _mediator.Send(new GetCarritoByIdQuery { CarritoSesionId = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewCarritoSesionCommand data)
        {
            return await _mediator.Send(data);
        }
    }
}
