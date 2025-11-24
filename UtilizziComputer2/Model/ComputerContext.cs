using DBUtilizziPC.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtilizziPC.Model
{
    internal class ComputerContext : DbContext
    {
        public DbSet<Classe> Classi { get; set; } = null!;
        public DbSet<Studente> Studenti { get; set; } = null!;
        public DbSet<Utilizza> Utilizzi { get; set; } = null!;
        public DbSet<Computer> Computers { get; set; } = null!;
        public string DbPath { get; } 

        public ComputerContext()
        {
            var appDir = AppContext.BaseDirectory;
            DbPath = Path.Combine(appDir, "../../../Utilizzi.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>()
                .HasMany(x => x.Studenti)
                .WithMany(x => x.Computers)
                .UsingEntity<Utilizza>();
        }


    }
}
