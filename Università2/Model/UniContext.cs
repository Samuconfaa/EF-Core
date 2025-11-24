using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using Università2.Data;

namespace Università2.Model
{
    public class UniContext : DbContext
    {
        public DbSet<Corso> Corsi { get; set; } = null!;
        public DbSet<CorsoLaurea> CorsiLaurea { get; set; } = null!;
        public DbSet<Docente> Docenti { get; set; } = null!;
        public DbSet<Frequenta> Frequenze { get; set; } = null!;
        public DbSet<Studente> Studenti { get; set; } = null!;
        public string DbPath { get; set; } = null!;

        public UniContext()
        {
            var appDir = AppContext.BaseDirectory;
            DbPath = Path.Combine(appDir, "../../../Uni.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converterLaurea = new EnumToStringConverter<TipoLaurea>();
            var converterFacoltà = new EnumToStringConverter<Facoltà>();
            var converterDipartimento = new EnumToStringConverter<Dipartimento>();

            modelBuilder.Entity<>

        }

    }
}
