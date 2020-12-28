using FluentValidation;
using TiendaServices.Api.Libro.Application;

namespace TiendaServices.Api.Autor.Application
{
    public class NewLibroCommandValidator : AbstractValidator<NewLibroCommand>
    {
        public NewLibroCommandValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty();
            RuleFor(x => x.FechaPublicacion).NotEmpty();
            RuleFor(x => x.AutorLibro).NotEmpty();
        }
    }
}
