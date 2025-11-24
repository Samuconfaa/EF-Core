using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UtilizziComputer3.Data;

namespace UtilizziComputer3.Model
{
    public class UtilizziContext : DbContext
    {
        public DbSet<Studente> Studenti { get; set; } = null!;
        public DbSet<Classe> Classe { get; set; } = null!;
        public DbSet<Utilizza> Utilizza { get; set; } = null!;
        public DbSet<Computer> Computer { get; set; } = null!;
        public string DbPath { get; } = null!;

        public UtilizziContext()
        {
            var appDir = AppContext.BaseDirectory;
            DbPath = Path.Combine(appDir, "../../../Utilizzi.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Studente>()
                .HasMany(x => x.Computer)
                .WithMany(x => x.Studenti)
                .UsingEntity<Utilizza>();
        }

    }
}
