using Microsoft.EntityFrameworkCore;
using MusicManager.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MusicManager.Model
{
    public class MusicContext : DbContext
    {
        public DbSet<Cantante> Cantanti { get; set; }
        public DbSet<Esibizione> Esibizioni { get; set; }
        public DbSet<Etichetta> Etichette { get; set; }
        public DbSet<Festival> Festival { get; set; }
        public string DbPath { get; } = null!;

        public MusicContext()
        {
            var appDir = AppContext.BaseDirectory;
            DbPath = Path.Combine(appDir, "../../../Musica.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cantante>()
                .HasMany(x => x.Festival)
                .WithMany(x => x.Cantanti)
                .UsingEntity<Esibizione>();
        }

    }
}
