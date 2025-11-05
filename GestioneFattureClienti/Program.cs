using GestioneFattureClienti.Data;
using GestioneFattureClienti.Model;
using System.Text;

namespace GestioneFattureClienti
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("it-IT");

            Console.ForegroundColor = ConsoleColor.Green;

            //1) Creazione dei dati nel Database ---> Create
            //Ora è commentato perchè va eseguito solo una volta, sennò le successive da errore
            //CreazioneDb();

            //2) Lettura dei dati nel Database ---> Read
            LetturaDb();

            //3) Modificare i dati nel Database ---> Update
            ModificaDb();

        }

        static void CreazioneDb()
        {
            //Creo un oggetto FattureClientiContext
            using var db = new FattureClientiContext();

            //Creazione dei clienti
            List<Cliente> listaClienti =
            [
                new (){ClienteId=1, RagioneSociale= "Cliente 1", PartitaIVA= "1111111111", Citta = "Napoli", Via="Via dei Mille", Civico= "23", CAP="80100"},
                new (){ClienteId=2, RagioneSociale= "Cliente 2", PartitaIVA= "1111111112", Citta = "Roma", Via="Via dei Fori Imperiali", Civico= "1", CAP="00100"},
                new (){ClienteId=3, RagioneSociale= "Cliente 3", PartitaIVA= "1111111113", Citta = "Firenze", Via="Via Raffaello", Civico= "10", CAP="50100"}
            ];

            //Creazione delle Fatture
            List<Fattura> listaFatture =
            [
                new (){FatturaId=1, Data= DateTime.Now.Date, Importo = 1200.45m, ClienteId = 1},
                new (){FatturaId=2, Data= DateTime.Now.AddDays(-5).Date, Importo = 3200.65m, ClienteId = 1},
                new (){FatturaId=3, Data= new DateTime(2019,10,20).Date, Importo = 5200.45m, ClienteId = 1},
                new (){FatturaId=4, Data= DateTime.Now.Date, Importo = 5200.45m, ClienteId = 2},
                new (){FatturaId=5, Data= new DateTime(2019,08,20).Date, Importo = 7200.45m, ClienteId = 2}
            ];

            Console.WriteLine("Inserimento clienti nel database...");
            listaClienti.ForEach(x => db.Add(x));
            db.SaveChanges();
            Console.WriteLine("Clienti inseriti.\nInserimento Fatture nel database...");
            listaFatture.ForEach(x => db.Add(x));
            db.SaveChanges();
            Console.WriteLine("Fatture inserite");

            Console.WriteLine("Database creato con successo");


        }

        static void LetturaDb()
        {
            using var db = new FattureClientiContext();

            //Ora vado a leggere TUTTI i dati del database, ma è molto inefficente
            //Nelle applicazioni reali si va a leggere solo quello che serve

            //1° metodo
            var listaClienti = db.Clienti.ToList();
            var listaFatture = db.Fatture.ToList();


            //2° metodo
            List<Cliente> listaClienti2 = [.. db.Clienti];
            List<Fattura> listaFatture2 = [.. db.Fatture];


            //stampa delle fatture
            Console.WriteLine("Stampa delle fatture: ");
            listaFatture.ForEach(Console.WriteLine);


            //Modo migliore di interagire con il database: Recupero solo ciò che mi serve
            //Questo metodo non fa rimanere in memoria i dati
            Console.WriteLine("\n\nStampa delle fatture con il modo ottimale: ");
            db.Fatture.ToList().ForEach(Console.WriteLine);


            //Filtro dei dati effettuato sul database
            Console.WriteLine("\n\nFatture emesse nel 2019");
            db.Fatture
                .Where(f => f.Data.Year == 2019)
                .ToList()
                .ForEach(x => Console.WriteLine(x));

            //Calcolare importo massimo, medio e minimo delle fatture emesse massimo 3 giorni fa
            Console.WriteLine("\n\n");
            //ottengo il numero di fatture presenti
            int numeroFatture = db.Fatture.Where(x => x.Data < DateTime.Now.AddDays(-3)).Count();
            var fatture3Giorni = db.Fatture
                .Where(x => x.Data < DateTime.Now.AddDays(-3));

            if (numeroFatture > 0)
            {
                Console.WriteLine("Importo massimo di una fattura: " + fatture3Giorni.Max(x => (double)x.Importo));
                Console.WriteLine("Importo minimo di una fattura: " + fatture3Giorni.Min(x => (double)x.Importo));
                Console.WriteLine($"Importo medio di una fattura: {fatture3Giorni.Average(x => (double)x.Importo):C2}");
            }


            //Trovare nome e indirizzo dei clienti che hanno speso più di 5000€

            //Metodo 1 - Where e Join
            Console.WriteLine("\n\nI clienti che hanno speso più di 5000 euro sono:");
            var clienti5000Piu = db.Fatture
                .Where(x => x.Importo > 5000)
                .Join(db.Clienti,
                    f => f.ClienteId,
                    c => c.ClienteId,
                    (f, c) => c
                )
                .Select(x => new
                {
                    NomeCliente = x.RagioneSociale,
                    Indirizzo = $"{x.Via} {x.Civico}, {x.Citta} CAP: {x.CAP}"
                })
                .Distinct()
                .ToList();

            clienti5000Piu.ForEach(Console.WriteLine);


            //Metodo 2 - Navigation Property
            Console.WriteLine("\n\nStampa dei clienti che hanno speso più di 5000€ con il metodo più efficace");
            var clienti5000Piu2 = db.Fatture
                .Where(x => x.Importo > 5000)
                .Select(x => new
                {
                    NomeCliente = x.Cliente.RagioneSociale,
                    Indirizzo = $"{x.Cliente.Via} {x.Cliente.Civico}, {x.Cliente.Citta} CAP: {x.Cliente.CAP}",
                    Importo = x.Importo

                })
                .Distinct()
                .ToList();

            clienti5000Piu2.ForEach(Console.WriteLine);


        }

        static void ModificaDb()
        {
            //per modificare una dato di un database bisogna prima creare un riferimento in memoria del dato 
            //(in questo caso della fattura da modificare)
            var db = new FattureClientiContext();

            //esempio: modificare i dati della fattura con id 1 - Find
            Console.WriteLine("\n\nModifica importo fattura");
            var fattura1 = db.Fatture.Find(1); //il metodo find utilizza la chiave primaria per effettuare la ricerca
            if (fattura1 is not null)
            {
                Console.WriteLine($"Importo prima della modifica = {fattura1.Importo:C2}");
                //modificare l'importo del 20%
                fattura1.Importo *= 1.2m;

                //per rendere effettiva la modifica:
                db.SaveChanges();
                Console.WriteLine($"Importo dopo della modifica = {fattura1.Importo:C2}");
            }

            //altro modo per recuperare oggetti dal database - Where
            //scontare del 10% le fatture eseguite negli ultimi 30 giorni
            Console.WriteLine("\n\nSconto del 10% le fatture degli ultimi 30 giorni");
            var fatture30G = db.Fatture
                .Where(x => x.Data > DateTime.Now.AddDays(-30));

            if (fatture30G.Count() > 0)
            {
                Console.WriteLine("Stampa fatture prima della modifica:");
                foreach (var item in fatture30G)
                {
                    Console.WriteLine(item);
                }

                foreach (var item in fatture30G)
                {
                    item.Importo *= 0.9m;
                }
                db.SaveChanges();

                Console.WriteLine("\n\nStampa fatture dopo la modifica:");
                foreach (var item in fatture30G)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }



}
