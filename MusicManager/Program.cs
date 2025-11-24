using MusicManager.Data;
using MusicManager.Model;
using System.Runtime.ConstrainedExecution;

namespace MusicManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Inizio ore 18:50
            //Fine creazione modello: 19:04
            //-------------------------------
            //Inizio Creazione Query ore 19:16
            //fine 19:34
            //tempo 14 + 18 = 32 min


            //Q1: Creare un metodo che riceve in input un punteggio minimo(es. 80) e stampa l'elenco delle Esibizioni di successo. Per ogni esibizione stampare: Nome d'Arte del cantante, Nome del Festival e Voti ottenuti, filtrando solo chi ha superato la soglia inserita.

            //Q2: Creare un metodo che riceve in input il nome di un'Etichetta e stampa l'elenco dei Festival in cui ha partecipato almeno un cantante di quell'etichetta (evitare duplicati nei nomi dei festival).

            //Q3: Trovare e stampare i nomi dei cantanti(e il relativo Festival) che hanno avuto l'onere di aprire il festival (ovvero quelli con OrdineUscita uguale a 1).

            //Q4: Stampare la classifica dei cantanti basata sulla media voti.Per ogni cantante mostrare il Nome d'Arte e la media aritmetica dei voti presi in tutte le sue esibizioni, ordinando dal più bravo (media più alta) al meno bravo.

            //Q5: Per ogni Festival presente nel database, stampare il nome del Festival e il punteggio più alto(Record) registrato in quel festival.

            //PopolaDb();

            WriteLine("Query 1");
            Q1(80);

            WriteLine("\n\nQuery 2");
            Q2("Sony Music");

            WriteLine("\n\nQuery 3");
            Q3();

            WriteLine("\n\nQuery 4");
            Q4();

            WriteLine("\n\nQuery 5");
            Q5();

        }

        private static void Q5()
        {
            using var db = new MusicContext();
            db.Festival
                .Select(x => new {Nome = x.Nome, PuntMax = x.Esibizioni.Max(x => x.VotiGiuria)})
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static void Q4()
        {
            using var db = new MusicContext();
            db.Cantanti
                .Select(x => new {NomeCantante = x.NomeArte, MediaVoti = x.Esibizioni.Average(x => x.VotiGiuria) })
                .OrderByDescending(x => x.MediaVoti)
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static void Q3()
        {
            using var db = new MusicContext();
            db.Esibizioni
                .Where(x => x.OrdineUscita == 1)
                .Select(x => new { NomeCantante = x.Cantante.NomeArte, Festival = x.Festival })
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static void Q2(string etichetta)
        {
            using var db = new MusicContext();
            db.Esibizioni
                .Where(x => x.Cantante.Etichetta.Nome == etichetta)
                .Select(x => x.Festival)
                .Distinct()
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static void Q1(int punteggio)
        {
            using var db = new MusicContext();
            db.Esibizioni
                .Where(x => x.VotiGiuria >= punteggio)
                .Select(x => new { NomeCantante = x.Cantante.NomeArte, NomeFestival = x.Festival.Nome, Voto = x.VotiGiuria })
                .ToList()
                .ForEach(Console.WriteLine);
        }

        static void PopolaDb()
        {
            using var db = new MusicContext();
            List<Etichetta> etichette =
            [
                new (){Id=1, Nome="Universal Music", SedeLegale="Milano"},
                new (){Id=2, Nome="Sony Music", SedeLegale="Roma"},
                new (){Id=3, Nome="Warner Music", SedeLegale="Milano"},
            ];

            List<Cantante> cantanti =
            [
                new (){Id = 1, NomeArte="The Rocker", NomeReale="Mario Rossi", EtichettaId=1},
                new (){Id = 2, NomeArte="Melody", NomeReale="Anna Verdi", EtichettaId=1},
                new (){Id = 3, NomeArte="Trap King", NomeReale="Luca Bianchi", EtichettaId=2},
                new (){Id = 4, NomeArte="Jazz Master", NomeReale="Paolo Neri", EtichettaId=2},
                new (){Id = 5, NomeArte="Pop Queen", NomeReale="Giulia Gialli", EtichettaId=3},
                new (){Id = 6, NomeArte="Indie Boy", NomeReale="Marco Blu", EtichettaId=3}
            ];

            List<Festival> festival =
            [
                new (){Id=1, Nome="Sanremo Giovani", DataInizio=DateTime.Today},
                new (){Id=2, Nome="Festivalbar", DataInizio=DateTime.Today.AddDays(-30)}
            ];

            List<Esibizione> esibizioni =
            [
                // Festival 1 (Sanremo)
                new (){CantanteId = 1, FestivalId = 1, VotiGiuria= 85, OrdineUscita = 1},
                new (){CantanteId = 2, FestivalId = 1, VotiGiuria = 92, OrdineUscita = 2},
                new (){CantanteId = 3, FestivalId = 1, VotiGiuria = 70, OrdineUscita = 3},
                new (){CantanteId = 4, FestivalId = 1, VotiGiuria = 60, OrdineUscita = 4},
                new (){CantanteId = 5, FestivalId = 1, VotiGiuria = 95, OrdineUscita = 5},
                new (){CantanteId = 6, FestivalId = 1, VotiGiuria = 50, OrdineUscita = 6},

	            // Festival 2 (Festivalbar)
	            new (){CantanteId = 4, FestivalId = 2, VotiGiuria = 88, OrdineUscita = 1},
                new (){CantanteId = 2, FestivalId = 2, VotiGiuria = 90, OrdineUscita = 2},
                new (){CantanteId = 1, FestivalId = 2, VotiGiuria = 75, OrdineUscita = 3},
                new (){CantanteId = 5, FestivalId = 2, VotiGiuria = 80, OrdineUscita = 4},
                new (){CantanteId = 6, FestivalId = 2, VotiGiuria = 65, OrdineUscita = 5},
                new (){CantanteId = 3, FestivalId = 2, VotiGiuria = 55, OrdineUscita = 6},
            ];

            foreach(var item in etichette)
            {
                db.Add(item);
            }
            db.SaveChanges();


            foreach (var item in festival)
            {
                db.Add(item);
            }
            db.SaveChanges();

            foreach (var item in cantanti)
            {
                db.Add(item);
            }
            db.SaveChanges();

            foreach (var item in esibizioni)
            {
                db.Add(item);
            }
            db.SaveChanges();

        }

        static void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
