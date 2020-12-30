using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TiendaServices.Api.Libro.Application;
using TiendaServices.Api.Libro.Models;
using TiendaServices.Api.Libro.Persistence;
using TiendaServices.RabbitMQ.Bus.EventQueue;
using TiendaServices.RabbitMQ.Bus.RabbitBus;
using Xunit;

namespace TiendaServices.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibroMaterial> GetFakeData()
        {
            // This method fills data using Genfu nuget package
            A.Configure<LibroMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibroMaterialId, () => { return Guid.NewGuid(); });

            var list = A.ListOf<LibroMaterial>(30);
            list[0].LibroMaterialId = Guid.Empty;

            return list;
        }

        private Mock<LibreriaContext> CreateContext()
        {
            var testData = GetFakeData().AsQueryable();
            // Replace Entity Framework's interface to be emulated in tests
            var dbSet = new Mock<DbSet<LibroMaterial>>();
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(testData.Provider);
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Expression).Returns(testData.Expression);
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.ElementType).Returns(testData.ElementType);
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.GetEnumerator()).Returns(testData.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibroMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibroMaterial>(testData.GetEnumerator()));

            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibroMaterial>(testData.Provider));

            var context = new Mock<LibreriaContext>();
            context.Setup(x => x.Libros).Returns(dbSet.Object);

            return context;
        }

        private IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            return mapperConfig.CreateMapper();
        }

        [Fact]
        public async void GetLibroById()
        {
            var mockContext = CreateContext();
            var mapper = CreateMapper();

            var request = new GetLibroByIdQuery();
            request.LibroId = Guid.Empty;

            var handler = new GetLibroByIdQueryHandler(mockContext.Object, mapper);
            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.True(result.LibroMaterialId == Guid.Empty);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetLibrosAsync()
        {
            var mockContext = CreateContext();
            var mapper = CreateMapper();

            var getLibroQueryHandler = new GetLibroQueryHandler(mockContext.Object, mapper);
            var getLibroQuery = new GetLibroQuery();

            var list = await getLibroQueryHandler.Handle(getLibroQuery, new System.Threading.CancellationToken());

            Assert.True(list.Any());
        }

        [Fact]
        public async void CreateLibro()
        {
            // Configure EF to be used in memory for testing purposes
            var options = new DbContextOptionsBuilder<LibreriaContext>()
                .UseInMemoryDatabase("LibrosDB")
                .Options;

            var context = new LibreriaContext(options);
            var rabbitEventBus = new Mock<IRabbitEventBus>();
            rabbitEventBus.Setup(x => x.Publish(new EmailEventQueue("test@test.com", "test title", "test content")));

            var request = new NewLibroCommand
            {
                Titulo = "Libro titulo",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };
            var handler = new NewLibroCommandHandler(context, rabbitEventBus.Object);

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(result != null);
        }
    }
}
