﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recepti.Context;

namespace Recepti.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20190319143639_Migration1")]
    partial class Migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Recepti.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Banovan");

                    b.Property<string>("Ime")
                        .IsRequired();

                    b.Property<string>("KorisnickoIme")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("Prezime")
                        .IsRequired();

                    b.Property<string>("Uloga")
                        .IsRequired();

                    b.HasKey("KorisnikId");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Recepti.Models.Recept", b =>
                {
                    b.Property<int>("ReceptId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumObjave");

                    b.Property<string>("Kategorija")
                        .IsRequired();

                    b.Property<int>("KorisnikId");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Priprema")
                        .IsRequired();

                    b.Property<bool>("Privatan");

                    b.Property<string>("Sastav")
                        .IsRequired();

                    b.Property<string>("SlikaURL")
                        .IsRequired();

                    b.HasKey("ReceptId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Recepti");
                });

            modelBuilder.Entity("Recepti.Models.Recept", b =>
                {
                    b.HasOne("Recepti.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
