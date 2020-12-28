using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServices.Api.Autor.Application;
using TiendaServices.Api.Autor.Models;

namespace TiendaServices.Api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAutores()
        {
            return await _mediator.Send(new GetAutorQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutor(string id)
        {
            return await _mediator.Send(new GetAutorByIdQuery { AutorGuid = id } );
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewAutorCommand data)
        {
            return await _mediator.Send(data);
        }
    }
}
