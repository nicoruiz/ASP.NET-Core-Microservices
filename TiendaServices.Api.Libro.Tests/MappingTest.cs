using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaServices.Api.Libro.Application;
using TiendaServices.Api.Libro.Models;

namespace TiendaServices.Api.Libro.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibroMaterial, LibroDto>();
        }
    }
}
