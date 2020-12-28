using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServices.Api.Libro.Application;

namespace TiendaServices.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDto>>> GetLibros()
        {
            return await _mediator.Send(new GetLibroQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDto>> GetLibro(Guid? id)
        {
            return await _mediator.Send(new GetLibroByIdQuery { LibroId = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewLibroCommand data)
        {
            return await _mediator.Send(data);
        }
    }
}
