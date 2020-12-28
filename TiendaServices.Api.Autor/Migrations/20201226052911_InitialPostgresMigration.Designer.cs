﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TiendaServices.Api.Autor.Persistence;

namespace TiendaServices.Api.Autor.Migrations
{
    [DbContext(typeof(AutorContext))]
    [Migration("20201226052911_InitialPostgresMigration")]
    partial class InitialPostgresMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TiendaServices.Api.Autor.Models.AutorLibro", b =>
                {
                    b.Property<int>("AutorLibroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("AutorLibroGuid")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("AutorLibroId");

                    b.ToTable("AutorLibros");
                });

            modelBuilder.Entity("TiendaServices.Api.Autor.Models.GradoAcademico", b =>
                {
                    b.Property<int>("GradoAcademicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AutorLibroId")
                        .HasColumnType("integer");

                    b.Property<string>("CentroAcademico")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaGrado")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GradoAcademicoGuid")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("GradoAcademicoId");

                    b.HasIndex("AutorLibroId");

                    b.ToTable("GradoAcademicos");
                });

            modelBuilder.Entity("TiendaServices.Api.Autor.Models.GradoAcademico", b =>
                {
                    b.HasOne("TiendaServices.Api.Autor.Models.AutorLibro", "AutorLibro")
                        .WithMany("ListaGradoAcademico")
                        .HasForeignKey("AutorLibroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}