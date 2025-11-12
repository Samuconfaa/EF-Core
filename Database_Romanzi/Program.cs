using Database_Romanzi.Data;
using Database_Romanzi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace Database_Romanzi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1: creare un metodo che prende in input la nazionalità e stampa gli autori che hanno la nazionalità specificata
            //Q2: creare un metodo che prende in input il nome e il cognome di un autore e stampa tutti i romanzi di quell’autore
            //Q3: creare un metodo che prende in input la nazionalità e stampa quanti romanzi di quella nazionalità sono presenti nel database
            //Q4: creare un metodo che per ogni nazionalità stampa quanti romanzi di autori di quella nazionalità sono presenti nel database
            //Q5: creare un metodo che stampa il nome dei personaggi presenti in romanzi di autori di una data nazionalità


            Inizio();
            Query1();
            Query2();
            Query3();
            Query4();
            Query5();
         
        }

        static void Inizio()
        {
            using var db = new RomanziContext();
            //verifico che il database esisti già

            if (db.Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                Console.WriteLine("Il database esiste già, vuoi crearne uno da zero? [Si | No]" +
                        "\nATTENZIONE: Tutti i dati verranno riformattati!");
                bool dbErase = Console.ReadLine().Equals("si", StringComparison.OrdinalIgnoreCase);
                if (dbErase)
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    PopulateDb(db);
                    Console.WriteLine("Database ricreato correttamente");
                }
            }
            else
            {
                Console.WriteLine("Il database non esiste, lo creo e lo popolo");
                db.Database.EnsureCreated();
                PopulateDb(db);
            }
        }

        static void PopulateDb(RomanziContext db)
        {
            //Almeno 5 autori di nazionalità compresa tra "Americana", "Belga", "Inglese".
            List<Autore> autori =
            [
                new (){AutoreId=1, Nome="Ernest",Cognome="Hemingway", Nazionalita="Americana"},//AutoreId=1
                    new (){AutoreId=2,Nome="Philip",Cognome="Roth", Nazionalita="Americana"},//AutoreId=2
                    new (){AutoreId=3,Nome="Thomas",Cognome="Owen", Nazionalita="Belga"},//AutoreId=3
                    new (){AutoreId=4,Nome="William",Cognome="Shakespeare", Nazionalita="Inglese"},//AutoreId=4
                    new (){AutoreId=5,Nome="Charles",Cognome="Dickens", Nazionalita="Inglese"},//AutoreId=5
                ];

            autori.ForEach(a => db.Add(a));
            db.SaveChanges();


            //Almeno 10 romanzi degli autori precedentemente inseriti
            List<Romanzo> romanzi =
            [
                new (){RomanzoId=1, Titolo="For Whom the Bell Tolls", AnnoPubblicazione=1940, AutoreId=1},//RomanzoId=1
                    new (){RomanzoId=2,Titolo="The Old Man and the Sea", AnnoPubblicazione=1952, AutoreId=1},
                    new (){RomanzoId=3,Titolo="A Farewell to Arms",AnnoPubblicazione=1929, AutoreId=1},
                    new (){RomanzoId=4,Titolo="Letting Go", AnnoPubblicazione=1962, AutoreId=2},
                    new (){RomanzoId=5,Titolo="When She Was Good", AnnoPubblicazione=1967, AutoreId=2},
                    new (){RomanzoId=6,Titolo="Destination Inconnue", AnnoPubblicazione=1942, AutoreId=3},
                    new (){RomanzoId=7,Titolo="Les Fruits de l'orage", AnnoPubblicazione=1984, AutoreId=3},
                    new (){RomanzoId=8,Titolo="Giulio Cesare", AnnoPubblicazione=1599, AutoreId=4},
                    new (){RomanzoId=9,Titolo="Otello", AnnoPubblicazione=1604, AutoreId=4},
                    new (){RomanzoId=10,Titolo="David Copperfield", AnnoPubblicazione=1849, AutoreId=5},
                ];
            romanzi.ForEach(r => db.Add(r));
            db.SaveChanges();


            //Almeno 5 personaggi presenti nei romanzi precedentemente inseriti
            List<Personaggio> personaggi =
            [
                new (){PersonaggioId=1, Nome="Desdemona", Ruolo="Protagonista", Sesso="Femmina", RomanzoId=9},//PersonaggioId=1
                    new (){PersonaggioId=2,Nome="Jago", Ruolo="Protagonista", Sesso="Maschio", RomanzoId=9},
                    new (){PersonaggioId=3,Nome="Robert", Ruolo="Protagonista", Sesso="Maschio", RomanzoId=1},
                    new (){PersonaggioId=4,Nome="Cesare", Ruolo="Protagonista", Sesso="Maschio", RomanzoId=8},
                    new (){PersonaggioId=5,Nome="David", Ruolo="Protagonista", Sesso="Maschio", RomanzoId=10}
                    ];
            personaggi.ForEach(p => db.Add(p));
            db.SaveChanges();
        }

        static void Query1()
        {
            using var db = new RomanziContext();
            //Q1: creare un metodo che prende in input la nazionalità e stampa gli autori che hanno la nazionalità specificata
            Console.WriteLine("\n\nDi quale nazionalità vuoi conoscere gli autori?");
            string? nazionalità = Console.ReadLine();

            var autoriPerNaz = db.Autori
                .Where(x => x.Nazionalita.ToLower() == nazionalità.ToLower())
                .ToList();

            Console.WriteLine("Gli autori di nazionalità {0} sono: ", nazionalità);
            foreach (var item in autoriPerNaz)
            {
                Console.WriteLine($"Nome : {item.Nome} Cognome : {item.Cognome}");
            }
        }

        static void Query2()
        {
            //Q2: creare un metodo che prende in input il nome e il cognome di un autore e stampa tutti i romanzi di quell’autore
            using var db = new RomanziContext();
            Console.WriteLine("\n\nInserisci nome e cognome di un autore e io ti dirò che romanzi ha scritto");
            Console.Write("Nome: ");
            string nome = Console.ReadLine().ToLower();
            Console.Write("Cognome: ");
            string cognome = Console.ReadLine().ToLower();

            //versione normale
            var romanziAutori = db.Autori
                .Where(x => x.Nome.ToLower() == nome.ToLower() && x.Cognome.ToLower() == cognome.ToLower())
                .Join(db.Romanzi,
                    a => a.AutoreId,
                    r => r.AutoreId,
                    (a, r) => r.Titolo
                )
                .ToList();

            Console.WriteLine($"I libri che ha scritto {nome} {cognome} sono:");
            romanziAutori.ForEach(Console.WriteLine);


            //versione con le navigation proprierty
            //Include serve a dire di mettere dentro anche i romanzi collegati agli autori filtrati

            /*
            var romanziAutori = db.Autori
                .Where(x => x.Nome.ToLower() == nome.ToLower() && x.Cognome.ToLower() == cognome.ToLower())
                .Include(x => x.Romanzi)
                .Select(x => x.diocane);
            */


        }

        static void Query3()
        {
            using var db = new RomanziContext();
            //Q3: creare un metodo che prende in input la nazionalità e stampa quanti romanzi di quella nazionalità sono presenti nel database
            Console.WriteLine("\n\nDi quale nazionalità vuoi conoscere i romanzi?");
            string? nazionalità = Console.ReadLine();

            var romanziPerNaz = db.Autori
                .Where(x => x.Nazionalita.ToLower() == nazionalità.ToLower())
                .Join(db.Romanzi,
                    a => a.AutoreId,
                    r => r.AutoreId,
                    (a, r) => r
                )
                .Count();
            Console.WriteLine($"I romanzi scritti da un autore di nazionalità {nazionalità} sono "+ romanziPerNaz);
        }

        static void Query4()
        {
            //Q4: creare un metodo che per ogni nazionalità stampa quanti romanzi di autori di quella nazionalità
            //sono presenti nel database

            using var db = new RomanziContext();
            var autoriPerNaz = db.Autori
                .Join(db.Romanzi,
                    a => a.AutoreId,
                    r => r.AutoreId,
                    (a, r) => new
                    {
                        r,
                        a.Nazionalita
                    }
                )
                .GroupBy(x => x.Nazionalita);

            foreach (var group in autoriPerNaz)
            {
                Console.WriteLine($"Di nazionalità {group.Key} ci sono {group.Count()} romanzi");
            }
        }

        static void Query5()
        {
            //Q5: creare un metodo che stampa il nome dei personaggi presenti in romanzi di autori di una data nazionalità
            using var db = new RomanziContext();
            Console.WriteLine("\n\nDi quale nazionalità vuoi conoscere i personaggi?");
            string? nazionalità = Console.ReadLine();

            var romanziPerNaz = db.Autori
                .Where(x => x.Nazionalita.ToLower() == nazionalità.ToLower())
                .Join(db.Romanzi,
                    a => a.AutoreId,
                    r => r.AutoreId,
                    (a, r) => r
                )
                .Join(db.Personaggi,
                    x => x.RomanzoId,
                    p => p.RomanzoId,
                    (x, p) => p
                );

            Console.WriteLine($"I personaggi di nazionalità {nazionalità} sono");
            foreach (var item in romanziPerNaz)
            {
                Console.WriteLine(item.Nome);
            }
        }

    }
}
