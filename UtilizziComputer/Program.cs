using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;
using UtilizziComputer.Data;
using UtilizziComputer.Model;

namespace UtilizziComputer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("it-IT");
            Console.ForegroundColor = ConsoleColor.Cyan;

            //Q1(string classe): Stampa a Console il numero di alunni della classe in input; ad esempio, stampa il numero di alunni della 4IA
            //Q2(): Stampa a Console il numero di alunni per ogni classe
            //Q3(): Stampa gli studenti che non hanno ancora restituito i computer(sono quelli collegati a Utilizza con DataOraFineUtilizzo pari a null)
            //Q4(string classe): Stampa l’elenco dei computer che sono stati utilizzati dagli studenti della classe specificata in input.Ad esempio, stampare l’elenco dei computer utilizzati dalla 4IA.Non mostrare ripetizioni nella stampa.
            //Q5(int computerId): Dato un computer(di cui si conosce l’Id) riporta l’elenco degli studenti che lo hanno usato negli ultimi 30 giorni, con l’indicazione della DataOraInizioUtilizzo, ordinando i risultati per classe e, a parità di classe, per data(mostrando prima le date più recenti)
            //Q6(): Stampa per ogni classe quanti utilizzi di computer sono stati fatti negli ultimi 30 giorni.
            //Q7(): Stampa la classe che ha utilizzato maggiormente i computer(quella con il maggior numero di utilizzi) negli ultimi 30 giorni.


            bool uscitaDalProgramma;
            do
            {
                //gestione della scelta 
                bool correctInput;
                do
                {
                    Console.Write("Inserire la modalità operativa [");
                    foreach (var elem in Enum.GetNames(typeof(ModalitàOperativa)))
                    {
                        Console.Write(elem + ", ");
                    }
                    Console.WriteLine("]");
                    correctInput = Enum.TryParse(Console.ReadLine(), true, out ModalitàOperativa modalitàOperativa);
                    if (correctInput)
                    {
                        switch (modalitàOperativa)
                        {
                            case ModalitàOperativa.CreazioneDb:
                                using (UtilizziContext db = new())
                                {
                                    CreazioneDb(db);
                                }
                                break;
                            case ModalitàOperativa.Q1:
                                Console.WriteLine("Q1: Contare gli alunni di una classe");
                                Console.WriteLine("Inserire la classe");
                                string? classe = Console.ReadLine()?.ToUpper();
                                if (classe != null)
                                {
                                    using UtilizziContext db = new();
                                    Q1(classe, db);
                                }
                                break;
                            case ModalitàOperativa.Q2:
                                Console.WriteLine("Q2: Riportare il numero di alunni per ogni classe");
                                using (UtilizziContext db = new())
                                {
                                    Q2(db);
                                }
                                break;
                            case ModalitàOperativa.Q3:
                                Console.WriteLine("Q3: Stampa gli studenti che non hanno ancora restituito i computer (sono quelli collegati a Utilizza con DataOraFineUtilizzo pari a null)");
                                using (UtilizziContext db = new())
                                {
                                    Q3(db);
                                }
                                break;
                            case ModalitàOperativa.Q4:
                                Console.WriteLine("Q4: Stampa l’elenco dei computer che sono stati utilizzati dagli studenti della classe specificata in input. ");
                                Console.WriteLine("Inserire la classe");
                                classe = Console.ReadLine()?.ToUpper();
                                if (classe != null)
                                {
                                    using UtilizziContext db = new();
                                    Q4(classe, db);
                                }
                                break;
                            case ModalitàOperativa.Q5:
                                Console.WriteLine("Q5: Dato un computer (di cui si conosce l’Id) riporta l’elenco degli studenti che lo hanno usato negli ultimi 30 giorni, con l'indicazione della DataOraInizioUtilizzo," +
                                    "ordinando i risultati per classe e, a parità di classe, per data (mostrando prima le date più recenti)");
                                Console.WriteLine("Inserire l'id del computer");
                                bool correctId = int.TryParse(Console.ReadLine(), out int computerId) && computerId >= 0;
                                if (correctId)
                                {
                                    using UtilizziContext db = new();
                                    Q5(computerId, db);
                                }
                                break;
                            case ModalitàOperativa.Q6:
                                Console.WriteLine("Q6: Stampa per ogni classe quanti utilizzi di computer sono stati fatti negli ultimi 30 giorni.");
                                using (UtilizziContext db = new())
                                {
                                    Q6(db);
                                }
                                break;
                            case ModalitàOperativa.Q7:
                                Console.WriteLine("Q7: Stampa le classi che hanno utilizzato maggiormente i computer (quelle con il maggior numero di utilizzi) " +
                                    "negli ultimi 30 giorni");
                                using (UtilizziContext db = new())
                                {
                                    Q7(db);
                                }
                                break;
                            case ModalitàOperativa.CancellazioneDb:
                                using (UtilizziContext db = new())
                                {
                                    //CancellazioneDb(db);
                                }
                                break;
                            default:
                                WriteLineWithColor("Non è stata impostata nessuna modalità operativa", ConsoleColor.Yellow);
                                break;
                        }
                    }
                    if (!correctInput)
                    {
                        Console.Clear();
                        WriteLineWithColor("Il valore inserito non corrisponde a nessuna opzione valida.\nI valori ammessi sono: [Creazione, Lettura, Modifica, Cancellazione, Nessuna]", ConsoleColor.Red);
                    }
                } while (!correctInput);
                Console.WriteLine("Uscire dal programma?[Si, No]");
                uscitaDalProgramma = Console.ReadLine()?.ToLower().StartsWith("si") ?? false;
                Console.Clear();
            } while (!uscitaDalProgramma);

        }



        static void CreazioneDb(UtilizziContext db)
        {
            if (db.GetService<IRelationalDatabaseCreator>().Exists())
            {
                WriteLineWithColor("Il database esiste già, vuoi ricrearlo da capo? Tutti i valori precedentemente inseriti verranno persi. [Si, No]", ConsoleColor.Red);
                bool cancellare = Console.ReadLine()?.ToLower().Equals("si") ?? false;
                if (cancellare)
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    PopulateDb(db);
                }
            }
            else
            {
                db.Database.EnsureCreated();
                PopulateDb(db);
            }

            static void PopulateDb(UtilizziContext db)
            {
                //Creazione dei Clienti - gli id vengono generati automaticamente come campi auto-incremento quando si effettua l'inserimento, tuttavia
                //è bene inserire esplicitamente l'id degli oggetti quando si procede all'inserimento massivo gli elementi mediante un foreach perché
                //EF core potrebbe inserire nel database gli oggetti in un ordine diverso rispetto a quello del foreach
                // https://stackoverflow.com/a/54692592
                // https://stackoverflow.com/questions/11521057/insertion-order-of-multiple-records-in-entity-framework/
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
        }

        static void Q1(string classe, UtilizziContext db)
        {
            //Q1(string classe): Stampa a Console il numero di alunni della classe in input; ad esempio, stampa il numero di alunni della 4IA
            var classeTrovata = db.Classi
                .Include(c => c.Studenti)
                .FirstOrDefault(c => c.Nome.ToLower() == classe.ToLower());

            if (classeTrovata == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"La classe {classe.ToUpper()} non esiste nel database.");
                Console.ResetColor();
                return;
            }

            int numeroStudenti = classeTrovata.Studenti.Count;

            Console.WriteLine($"Nella classe {classe.ToUpper()} ci sono {numeroStudenti} studenti");

        }

        static void Q2(UtilizziContext db)
        {
            //Q2(): Stampa a Console il numero di alunni per ogni classe
            var classi = db.Classi
                .Include(x => x.Studenti);

            foreach (var item in classi)
            {
                Console.WriteLine($"Nella classe {item.Nome} ci sono {item.Studenti.Count} studenti");
            }
        }

        static void Q3(UtilizziContext db)
        {
            //Q3(): Stampa gli studenti che non hanno ancora restituito i computer(sono quelli collegati a Utilizza con DataOraFineUtilizzo pari a null)
            var noRest = db.Utilizzi
                .Where(x => x.DataOraFineUtilizzo == null)
                .Include(x => x.Studente)
                .ThenInclude(s => s.Classe) 
                .Include(u => u.Computer)
                .ToList();


            if (!noRest.Any())
            {
                WriteLineWithColor("Tutti gli studenti hanno riconsegnato i computer", ConsoleColor.Green);
            }

            foreach (var utilizzo in noRest)
            {
                Console.WriteLine(
                    $"Studente: {utilizzo.Studente.Nome} {utilizzo.Studente.Cognome} " +
                    $"({utilizzo.Studente.Classe.Nome}) - Computer: {utilizzo.Computer.Modello} " +
                    $"({utilizzo.Computer.Collocazione}) - Iniziato il {utilizzo.DataOraInizioUtilizzo:g}");
            }
        }
        
        static void Q4(string classe, UtilizziContext db)
        {
            //Q4(string classe): Stampa l’elenco dei computer che sono stati utilizzati dagli studenti della classe specificata in input.Ad esempio,
            //stampare l’elenco dei computer utilizzati dalla 4IA.Non mostrare ripetizioni nella stampa.

            //qua mi restituisce la classe corrispondente a quella richiesta caricata di tutti gli oggetti
            //che servono a calcolare le informazioni
            var classeSelezionata = db.Classi
                .Where(x => x.Nome.ToLower() == classe.ToLower())
                .Include(x => x.Studenti)
                    .ThenInclude(s => s.Utilizzi)
                        .ThenInclude(x => x.Computer)
                .FirstOrDefault();

            if (classeSelezionata == null)
            {
                WriteLineWithColor($"La classe {classe.ToUpper()} non esiste nel database.", ConsoleColor.Red);
                return;
            }

            //qua avviene davvero la ricerca che cerca tra tutti i computer solo
            //quelli che sono stati usati dagli studenti della classe selezionata
            var computerUsati = classeSelezionata.Studenti
                //prendo tutti gli utilizzi (anche di studenti diversi) e li metto in una unica lista
                .SelectMany(s => s.Utilizzi)
                //per ogni utilizzo ottenuto, mi interessa solo l'oggetto computer
                .Select(u => u.Computer)
                //prendo computer distinti
                .Distinct()
                .ToList();


            foreach (var item in computerUsati)
            {
                Console.WriteLine(item);
            }

        }

        static void Q5(int computerId, UtilizziContext db)
        {
            //Q5(int computerId): Dato un computer(di cui si conosce l’Id) riporta l’elenco degli studenti che lo hanno usato negli ultimi 30 giorni, con l’indicazione della DataOraInizioUtilizzo, ordinando i risultati per classe e, a parità di classe, per data(mostrando prima le date più recenti)
            var oggi = DateTime.Now;
            var trenta = DateTime.Now.AddDays(-30);

            var computerUsati = db.Utilizzi
                .Where(x => x.DataOraInizioUtilizzo >= trenta && x.ComputerId == computerId)
                .Include(x => x.Studente)
                    .ThenInclude(x => x.Classe)
                .Include(x => x.Computer)
                .OrderBy(x => x.Studente.Classe.Nome)
                    .ThenByDescending(x => x.DataOraInizioUtilizzo)
                .ToList();

            if (!computerUsati.Any())
            {
                WriteLineWithColor($"Nessuno studente ha usato il computer con ID {computerId} negli ultimi 30 giorni.", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine($"\nElenco utilizzi del computer {computerUsati.First().Computer.Modello} ({computerUsati.First().Computer.Collocazione}) negli ultimi 30 giorni:\n");

            foreach (var u in computerUsati)
            {
                Console.WriteLine(
                    $"Classe: {u.Studente.Classe.Nome} - " +
                    $"Studente: {u.Studente.Nome} {u.Studente.Cognome} - " +
                    $"Data inizio: {u.DataOraInizioUtilizzo:g}");
            }

        }

        static void Q6(UtilizziContext db)
        {
            var oggi = DateTime.Now;
            var trenta = DateTime.Now.AddDays(-30);

            //Q6(): Stampa per ogni classe quanti utilizzi di computer sono stati fatti negli ultimi 30 giorni.
            var utilizziClasse = db.Classi
                .Include(x => x.Studenti)
                    .ThenInclude (x => x.Utilizzi)
                .Select(x => new
                {
                    NomeClasse = x.Nome,
                    NumUtilizzi = x.Studenti
                        .SelectMany(x => x.Utilizzi)
                        .Count(x => x.DataOraInizioUtilizzo >= trenta)
                })
                .OrderByDescending(x => x.NumUtilizzi)
                .ToList();

            foreach (var item in utilizziClasse)
            {
                Console.WriteLine($"Classe: {item.NomeClasse} , Numero Utilizzi: {item.NumUtilizzi}");
            }
        }

        static void Q7(UtilizziContext db)
        {
            //Q7(): Stampa la classe che ha utilizzato maggiormente i computer(quella con il maggior numero di utilizzi) negli ultimi 30 giorni.
            var oggi = DateTime.Now;
            var trenta = DateTime.Now.AddDays(-30);

            var utilizziClasse = db.Classi
                .Include(x => x.Studenti)
                    .ThenInclude(x => x.Utilizzi)
                .Select(x => new
                {
                    NomeClasse = x.Nome,
                    NumUtilizzi = x.Studenti
                        .SelectMany(x => x.Utilizzi)
                        .Count(x => x.DataOraInizioUtilizzo >= trenta)
                })
                .OrderByDescending(x => x.NumUtilizzi)
                .First();

            Console.WriteLine("La classe che ha usato più volte il computer è la " + utilizziClasse.NomeClasse);
        }

        static void WriteLineWithColor(string text, ConsoleColor consoleColor)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = previousColor;
        }

        enum ModalitàOperativa
        {
            CreazioneDb,
            Q1,
            Q2,
            Q3,
            Q4,
            Q5,
            Q6,
            Q7,
            Nessuna,
            CancellazioneDb
        }
    }
}
