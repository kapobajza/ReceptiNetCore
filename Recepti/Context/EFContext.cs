using Microsoft.EntityFrameworkCore;
using Recepti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Recept> Recepti { get; set; }
        public DbSet<ErrorLogging> ErrorLogging { get; set; }
        public DbSet<Audit> Audit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Korisnik>()
                .HasIndex(x => x.KorisnickoIme)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
