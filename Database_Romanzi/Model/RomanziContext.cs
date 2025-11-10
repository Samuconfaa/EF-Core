using Database_Romanzi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Romanzi.Model
{
    internal class RomanziContext : DbContext
    {
        public DbSet<Autore> Autori { get; set; } = null!;
        public DbSet<Personaggio> Personaggi { get; set; } = null!;
        public DbSet<Romanzo> Romanzi { get; set; } = null!;
        public string DbPath { get; }

        public RomanziContext()
        {
            var appDir = AppContext.BaseDirectory;
            var path = Path.Combine(appDir, "../../../Romanzi.db");
            DbPath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
        }

    }
}
