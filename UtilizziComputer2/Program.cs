using DBUtilizziPC.Data;
using DBUtilizziPC.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DBUtilizziPC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1(string classe): Stampa a Console il numero di alunni della classe in input; ad esempio, stampa il numero di alunni della 4IA
            //Q2(): Stampa a Console il numero di alunni per ogni classe
            //Q3(): Stampa gli studenti che non hanno ancora restituito i computer(sono quelli collegati a Utilizza con DataOraFineUtilizzo pari a null)
            //Q4(string classe): Stampa l’elenco dei computer che sono stati utilizzati dagli studenti della classe specificata in input.Ad esempio,
            //stampare l’elenco dei computer utilizzati dalla 4IA.Non mostrare ripetizioni nella stampa.
            //Q5(int computerId): Dato un computer(di cui si conosce l’Id) riporta l’elenco degli studenti che lo hanno usato negli ultimi 30 giorni,
            //con l’indicazione della DataOraInizioUtilizzo, ordinando i risultati per classe e, a parità di classe, per data(mostrando prima le date più recenti)
            //Q6(): Stampa per ogni classe quanti utilizzi di computer sono stati fatti negli ultimi 30 giorni.
            //Q7(): Stampa la classe che ha utilizzato maggiormente i computer(quella con il maggior numero di utilizzi) negli ultimi 30 giorni.

            //PopolaDb();

            Console.WriteLine("Q1...");
            Q1("4IA");

            Console.WriteLine("\n\nQ2...");
            Q2();

            Console.WriteLine("\n\nQ3...");
            Q3();

            Console.WriteLine("\n\nQ4...");
            Q4("5IA");

            Console.WriteLine("\n\nQ5...");
            Q5(1);

            Console.WriteLine("\n\nQ6...");
            Q6();

            Console.WriteLine("\n\nQ7...");
            Q7();

        }

        static void PopolaDb()
        {
            using var db = new ComputerContext();
            List<Classe> classi =
           [
               new (){Id =1, Nome="3IA", Aula="Est 1"},
                new (){Id =2,Nome="4IA", Aula="A32"},
                new (){Id =3,Nome="5IA", Aula="A31"},
                new (){Id =4,Nome="3IB", Aula="Est 2"},
                new (){Id =5,Nome="4IB", Aula="A30"},
                new (){Id =6,Nome="5IB", Aula="A32"},
            ];

            List<Studente> studenti =
           [
               new (){Id = 1, Nome = "Mario", Cognome = "Rossi", ClasseId =1 },
                new (){Id = 2, Nome = "Giovanni", Cognome = "Verdi", ClasseId =1 },
                new (){Id = 3, Nome = "Piero", Cognome = "Angela", ClasseId = 1 },
                new (){Id = 4, Nome = "Leonardo", Cognome = "Da Vinci", ClasseId = 1 },
                new (){Id = 50, Nome = "Cristoforo", Cognome = "Colombo", ClasseId=2 },
                new (){Id = 51, Nome = "Piero", Cognome = "Della Francesca", ClasseId=2 },
                new (){Id = 82, Nome = "Alessandro", Cognome = "Manzoni", ClasseId=4 },
                new (){Id = 83, Nome = "Giuseppe", Cognome = "Parini", ClasseId=4 },
                new (){Id = 102, Nome = "Giuseppe", Cognome = "Ungaretti", ClasseId=3 },
                new (){Id = 103, Nome = "Luigi", Cognome = "Pirandello", ClasseId=3 },
                new (){Id = 131, Nome = "Enrico", Cognome = "Fermi", ClasseId=6 },
                new (){Id = 132, Nome = "Sandro", Cognome = "Pertini", ClasseId=6 },
            ];

            List<Computer> computers =
           [
               new (){Id = 1, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D1-D5"},
                new (){Id = 2, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D1-D5"},
                new (){Id = 3, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D1-D5"},
                new (){Id = 4, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D1-D5"},
                new (){Id = 5, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D1-D5"},
                new (){Id = 6, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D6-D10"},
                new (){Id = 7, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D6-D10"},
                new (){Id = 8, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D6-D10"},
                new (){Id = 9, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D6-D10"},
                new (){Id = 10, Modello="Hp 19 inc. 2019", Collocazione = "Bunker-D6-D10"},
                new (){Id = 20, Modello="Lenovo i5 2020", Collocazione = "Bunker-D20-D25"},
                new (){Id = 21, Modello="Lenovo i5 2020", Collocazione = "Bunker-D20-D25"},
                new (){Id = 22, Modello="Lenovo i5 2020", Collocazione = "Bunker-D20-D25"},
                new (){Id = 23, Modello="Lenovo i5 2020", Collocazione = "Bunker-D20-D25"},
                new (){Id = 24, Modello="Lenovo i5 2020", Collocazione = "Bunker-D20-D25"},
                new (){Id = 61, Modello="Lenovo i5 2021", Collocazione = "Carrello-Mobile-S1"},
                new (){Id = 62, Modello="Lenovo i5 2021", Collocazione = "Carrello-Mobile-S2"},
                new (){Id = 63, Modello="Lenovo i5 2021", Collocazione = "Carrello-Mobile-S3"},
                new (){Id = 64, Modello="Lenovo i5 2021", Collocazione = "Carrello-Mobile-S4"},
                new (){Id = 65, Modello="Lenovo i5 2021", Collocazione = "Carrello-Mobile-S5"},
            ];

            List<Utilizza> utilizzi = new()
            {
                new (){ComputerId = 61,StudenteId=1,
                    DataOraInizioUtilizzo = DateTime.Now.Add(- new TimeSpan(1,12,0)),
                    DataOraFineUtilizzo = DateTime.Now},
                new (){ComputerId = 61,StudenteId=1,
                    DataOraInizioUtilizzo = DateTime.Now.Add(- new TimeSpan(1,1,12,0)),
                    DataOraFineUtilizzo = DateTime.Now.Add(- new TimeSpan(1,0,0,0))},
                new (){ComputerId = 61,StudenteId=3,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-2).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-2).AddHours(12)},
                new (){ComputerId = 61,StudenteId=82,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(12),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(13) },
                new (){ComputerId = 61,StudenteId=1,
                    DataOraInizioUtilizzo = DateTime.Today.AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddHours(12) },
                new (){ComputerId = 62,StudenteId=2,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-2).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-2).AddHours(12) },
                new (){ComputerId = 62,StudenteId=2,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(12),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(13) },
                new (){ComputerId = 62,StudenteId=4,
                    DataOraInizioUtilizzo = DateTime.Today.AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddHours(11) },
                new (){ComputerId = 1,StudenteId=50,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-2).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-2).AddHours(12) },
                new (){ComputerId = 1,StudenteId=103,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(12),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(13) },
                new (){ComputerId = 1,StudenteId=50,
                    DataOraInizioUtilizzo = DateTime.Today.AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddHours(12) },
                new (){ComputerId = 2,StudenteId=51,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(12) },
                new (){ComputerId = 2,StudenteId=51,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(12),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(13) },
                new (){ComputerId = 2,StudenteId=103,
                    DataOraInizioUtilizzo = DateTime.Today.AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddHours(12) },
                new (){ComputerId = 3,StudenteId=82,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-2).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-2).AddHours(12) },
                new (){ComputerId = 3,StudenteId=82,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(13) },
                new (){ComputerId = 3,StudenteId=83,
                    DataOraInizioUtilizzo = DateTime.Today.AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddHours(12) },
                new (){ComputerId = 20,StudenteId=102,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-2).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-2).AddHours(12) },
                new (){ComputerId = 20,StudenteId=103,
                    DataOraInizioUtilizzo = DateTime.Today.AddDays(-1).AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddDays(-1).AddHours(12) },
                new (){ComputerId = 20,StudenteId=103,
                    DataOraInizioUtilizzo = DateTime.Today.AddHours(11),
                    DataOraFineUtilizzo = DateTime.Today.AddHours(12) },
                new (){ComputerId = 64,StudenteId=131,
                    DataOraInizioUtilizzo = DateTime.Now.Add(- new TimeSpan(0,12,0)),
                    DataOraFineUtilizzo = null},
                new (){ComputerId = 65,StudenteId=132,
                    DataOraInizioUtilizzo = DateTime.Now.Add(- new TimeSpan(1,12,0)),
                    DataOraFineUtilizzo = null},
            };
            Console.WriteLine("Inseriamo le classi nel database");
            classi.ForEach(c => db.Add(c));
            db.SaveChanges();
            Console.WriteLine("Inseriamo gli studenti nel database");
            studenti.ForEach(s => db.Add(s));
            db.SaveChanges();
            Console.WriteLine("Inseriamo i computers nel database");
            computers.ForEach(c => db.Add(c));
            db.SaveChanges();
            Console.WriteLine("Inseriamo gli utilizzi nel database");
            utilizzi.ForEach(u => db.Add(u));
            db.SaveChanges();

        }

        static void Q1(string classe)
        {
            //Q1(string classe): Stampa a Console il numero di alunni della classe in input; ad esempio, stampa il numero di alunni della 4IA
            using var db = new ComputerContext();
            Classe? laClasse = db.Classi.Where(x => x.Nome == classe).FirstOrDefault();
                
            if (laClasse == null)
            {
                Console.WriteLine("Non esiste nessuna classe con quel nome");
            }
            else
            {
                int numStud = db.Studenti.Where(x => x.ClasseId == laClasse.Id).Count();
                Console.WriteLine($"Nella classe {classe} ci sono {numStud} studenti");

            }

        }

        static void Q2()
        {
            //Q2(): Stampa a Console il numero di alunni per ogni classe
            using var db = new ComputerContext();
            db.Studenti
                .GroupBy(x => x.Classe)
                .Select(x => new { x.Key, NumStud = x.Count() })
                .ToList()
                .ForEach(x => Console.WriteLine($"Nella classe {x.Key.Nome} ci sono {x.NumStud} studenti"));
        }

        static void Q3()
        {
            using var db = new ComputerContext();
            //Q3(): Stampa gli studenti che non hanno ancora restituito i computer(sono quelli collegati a Utilizza con DataOraFineUtilizzo pari a null)
            db.Utilizzi
                .Where(x=> x.DataOraFineUtilizzo == null)
                .Include(x => x.Studente.Classe)
                .Select(x => x.Studente)
                .ToList()
                .ForEach(Console.WriteLine);
        }

        static void Q4(string classe)
        {
            //Q4(string classe): Stampa l’elenco dei computer che sono stati utilizzati dagli studenti della classe specificata in input.Ad esempio,
            //stampare l’elenco dei computer utilizzati dalla 4IA.Non mostrare ripetizioni nella stampa.
            using var db = new ComputerContext();
            db.Studenti
                .Where(x => x.Classe.Nome == classe)
                .SelectMany(x => x.Computers)
                .Distinct()
                .ToList()
                .ForEach(Console.WriteLine);
        }

        static void Q5(int computerId)
        {
            using var db = new ComputerContext();
            //Q5(int computerId): Dato un computer(di cui si conosce l’Id) riporta l’elenco degli studenti che lo hanno usato negli ultimi 30 giorni,
            //con l’indicazione della DataOraInizioUtilizzo, ordinando i risultati per classe e, a parità di classe, per data(mostrando prima le date più recenti)
            db.Utilizzi
                .Where(x => x.DataOraInizioUtilizzo >= DateTime.Now.AddDays(-30) && x.ComputerId == computerId)
                .Include(x => x.Studente)
                .ThenInclude(x => x.Classe)
                .OrderBy(x => x.Studente.Classe.Nome)
                .ThenByDescending(x => x.DataOraInizioUtilizzo)
                .ToList()
                .ForEach(x => Console.WriteLine($"Studente {x.Studente} = DataOraInizio {x.DataOraInizioUtilizzo}"));
        }

        static void Q6()
        {
            using var db = new ComputerContext();
            //Q6(): Stampa per ogni classe quanti utilizzi di computer sono stati fatti negli ultimi 30 giorni.
            db.Utilizzi
                .Where(x => x.DataOraInizioUtilizzo >= DateTime.Now.AddDays(-30))
                .GroupBy(x => x.Studente.Classe.Nome)
                .Select(x => new { Classe = x.Key, NumUt = x.Count() })
                .ToList()
                .ForEach(x => Console.WriteLine($"Classe {x.Classe}, Utilizzi {x.NumUt}"));
        }

        static void Q7()
        {
            using var db = new ComputerContext();
            //Q7(): Stampa la classe che ha utilizzato maggiormente i computer(quella con il maggior numero di utilizzi) negli ultimi 30 giorni.
            var classe = db.Utilizzi
                .Where(x => x.DataOraInizioUtilizzo >= DateTime.Now.AddDays(-30))
                .GroupBy(x => x.Studente.Classe.Nome)
                .Select(x => new { Nome = x.Key, Num = x.Count() })
                .OrderByDescending(x => x.Num)
                .First();

            Console.WriteLine(classe.Nome);
        }
    }
}
