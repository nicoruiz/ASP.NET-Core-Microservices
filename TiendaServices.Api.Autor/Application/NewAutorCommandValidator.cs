using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServices.Api.Autor.Application
{
    public class NewAutorCommandValidator : AbstractValidator<NewAutorCommand>
    {
        public NewAutorCommandValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Apellido).NotEmpty();
        }
    }
}
