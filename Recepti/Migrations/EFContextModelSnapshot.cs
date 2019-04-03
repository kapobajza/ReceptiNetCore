﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recepti.Context;

namespace Recepti.Migrations
{
    [DbContext(typeof(EFContext))]
    partial class EFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Recepti.Models.Audit", b =>
                {
                    b.Property<int>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AccessDate");

                    b.Property<string>("ActionName");

                    b.Property<string>("ControllerName");

                    b.Property<string>("IpAddress");

                    b.Property<bool>("IsLoggedIn");

                    b.Property<int?>("KorisnikId");

                    b.Property<DateTime?>("LoggedInAt");

                    b.Property<DateTime?>("LoggedOutAt");

                    b.Property<string>("Method");

                    b.Property<string>("PageAccessed");

                    b.Property<int>("ResponseStatusCode");

                    b.Property<string>("UrlReferrer");

                    b.HasKey("AuditId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Recepti.Models.ErrorLogging", b =>
                {
                    b.Property<int>("ErrorLoggingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Message");

                    b.Property<string>("OriginalBasePath");

                    b.Property<string>("OriginalQueryString");

                    b.Property<string>("Path");

                    b.Property<string>("StackTrace");

                    b.Property<int?>("StatusCode");

                    b.HasKey("ErrorLoggingId");

                    b.ToTable("ErrorLogging");
                });

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

                    b.HasIndex("KorisnickoIme")
                        .IsUnique();

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

            modelBuilder.Entity("Recepti.Models.Audit", b =>
                {
                    b.HasOne("Recepti.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId");
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
