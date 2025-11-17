using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using Università.Model;

namespace Università.Data
{
    public class UniContext : DbContext
    {
        public DbSet<Corso> Corsi { get; set; } = null!;
        public DbSet<CorsoLaurea> CorsiLauree { get; set; } = null!;
        public DbSet<Docente> Docenti { get; set; } = null!;
        public DbSet<Frequenta> Frequenze { get; set; } = null!;
        public DbSet<Studente> Studenti { get; set; } = null!;
        public string DbPath { get; } = null!;

        public UniContext()
        {
            var appDir = AppContext.BaseDirectory;
            DbPath = Path.Combine(appDir, "../../../Uni.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converterTipoLaurea = new EnumToStringConverter<TipoLaurea>();
            var converterDipartimento = new EnumToStringConverter<Dipartimento>();
            var converterFacoltà = new EnumToStringConverter<Facoltà>();

            modelBuilder
                .Entity<CorsoLaurea>()
                .Property(z => z.Facoltà)
                .HasConversion(converterFacoltà);

            modelBuilder
                .Entity<CorsoLaurea>()
                .Property(z => z.TipoLaurea)
                .HasConversion(converterTipoLaurea);

            modelBuilder
                .Entity<Docente>()
                .Property(z => z.Dipartimento)
                .HasConversion(converterDipartimento);

            modelBuilder
                .Entity<Corso>()
                .HasMany(x => x.Studenti)
                .WithMany(x => x.Corsi)
                .UsingEntity<Frequenta>
                (
                    left => left
                        .HasOne(x => x.Studente)
                        .WithMany(x => x.Frequenze)
                        .HasForeignKey(x => x.Matricola),

                    right => right
                        .HasOne(x => x.Corso)
                        .WithMany(x => x.Frequenze)
                        .HasForeignKey(x => x.CodCorso),

                    k => k.HasKey(fr => new { fr.Matricola, fr.CodCorso })

                );

        }


    }
}
