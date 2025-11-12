using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilizziComputer.Data;

namespace UtilizziComputer.Model
{
    internal class UtilizziContext:DbContext
    {
        public DbSet<Classe> Classi { get; set; }
        public DbSet<Computer> Computer { get; set; }
        public DbSet<Studente> Studente { get; set; }
        public DbSet<Utilizza> Utilizzi { get; set; }
        public string DbPath { get; }

        public UtilizziContext()
        {
            var path = AppContext.BaseDirectory;
            DbPath = Path.Combine(path, "../../../UtilizziPC.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Studente>()
                .HasMany(s => s.Computers)
                .WithMany(c => c.Studenti)
                .UsingEntity<Utilizza>(
            );
        }




    }
}
