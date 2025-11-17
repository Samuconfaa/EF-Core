using static System.Runtime.InteropServices.JavaScript.JSType;
using Università.Model;
using Microsoft.EntityFrameworkCore.Storage;
using Università.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Università
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1: Stampare l’elenco degli studenti
            //Q2: Stampare l’elenco dei corsi
            //Q3: Modificare il docente di un corso di cui è noto l’id
            //Q4: Stampare il numero di corsi seguiti dallo studente con id = 1
            //Q5: Stampare il numero di corsi seguiti dallo studente con Nome =“Giovanni” e Cognome =“Casiraghi”
            //Q6: Stampare il numero di corsi seguiti da ogni studente
            //Q7: Stampare i corsi seguiti dallo studente con Nome =“Piero” e Cognome =“Gallo”

            InitTest();
            WriteLineWithColor("\nEsecuzione di Q1:" +
                "\nStampare l'elenco degli studenti: ", ConsoleColor.Cyan);
            Q1();
            WriteLineWithColor("\nEsecuzione di Q2:" +
                "\nStampare l'elenco dei corsi", ConsoleColor.Cyan);
            Q2();
            WriteLineWithColor("\nEsecuzione di Q3:" +
                "\nModificare il docente di un corso di cui è noto l'id:", ConsoleColor.Cyan);
            Q3(1, 3);
            WriteLineWithColor("\nDopo la modifica i corsi sono i seguenti:", ConsoleColor.Cyan);
            Q2();
            WriteLineWithColor("\nEsecuzione di Q4:" +
                "\nStampare il numero di corsi seguiti dallo studente con id = 1:", ConsoleColor.Cyan);
            Q4(1);
            WriteLineWithColor("\nEsecuzione di Q5:" +
                 "\nStampare il numero di corsi seguiti dallo studente con Nome=\"Giovanni\" e Cognome =\"Casiraghi\"", ConsoleColor.Cyan);
            Q5("Giovanni", "Casiraghi");
            WriteLineWithColor("\nEsecuzione di Q6:" +
                "\nStampare il numero di corsi seguiti da ogni studente", ConsoleColor.Cyan);
            Q6();
            WriteLineWithColor("\nEsecuzione di Q7:" +
                "\nStampare i corsi seguiti dallo studente con Nome=\"Piero\" e Cognome =\"Gallo\"", ConsoleColor.Cyan);
            Q7("Piero", "Gallo");
            WriteLineWithColor("Finito!", ConsoleColor.Cyan);
        }

        static void Q1()
        {
            //Q1: Stampare l’elenco degli studenti
            using var db = new UniContext();
            var studenti = db.Studenti.ToList();
            studenti.ForEach(Console.WriteLine);
        }

        static void Q2()
        {
            using var db = new UniContext();
            //Q2: Stampare l’elenco dei corsi

            List<Corso> corsi = [.. db.Corsi];
            corsi.ForEach(s => Console.WriteLine($"CodCorso = {s.CodiceCorso}, Nome = {s.Nome}, CodDocente = {s.CodDocente}"));
        }

        static void Q3(int codCorso, int newCodDoc)
        {
            using var db = new UniContext();
            //Q3: Modificare il docente di un corso di cui è noto l’id
            var corso = db.Corsi.Find(codCorso);

            var docenteNew = db.Docenti.Find(newCodDoc);
            if (docenteNew is null)
            {
                WriteLineWithColor("Non esiste nessuno docente con quell'id", ConsoleColor.Red);
                return;
            }else if(corso is null)
            {
                WriteLineWithColor("Non esiste nessun corso con quell'id", ConsoleColor.Red);
                return;
            }

            corso.CodDocente = newCodDoc;
            db.SaveChanges();



        }

        static void Q4(int matr)
        {
            using var db = new UniContext();
            //Q4: Stampare il numero di corsi seguiti dallo studente con id = 1
            var corsiStud = db.Frequenze.Where(x => x.Matricola == matr).Count();

            Console.WriteLine($"Numero corsi frequentati dallo studente con Matricola = {matr} " +
            $"-> numero corsi: {corsiStud}");
        }

        static void Q5(string nome, string cognome)
        {
            using var db = new UniContext();
            //Q5: Stampare il numero di corsi seguiti dallo studente con Nome =“Giovanni” e Cognome =“Casiraghi”

            var corsiStud = db.Frequenze
                .Include(x => x.Studente)
                .Include(x => x.Corso)
                .Where(x => x.Studente.Nome == nome && x.Studente.Cognome == cognome)
                .Count();

            Console.WriteLine(corsiStud);
            
        }

        static void Q6()
        {
            //Q6: Stampare il numero di corsi seguiti da ogni studente
            using var db = new UniContext();

            var corsiStud = db.Frequenze
                .Include(x => x.Studente)
                .Include(x => x.Corso)
                .GroupBy(x => new { x.Studente.Nome, x.Studente.Cognome, x.Studente.Matricola })
                .Select(x => new
                {
                    Studente = x.Key,
                    NumCorsi = x.Count()
                })
                .ToList();

            foreach (var s in corsiStud)
            {
                Console.WriteLine($"{s.Studente.Nome} {s.Studente.Cognome} segue {s.NumCorsi} corsi");
            }
        }

        static void Q7(string nome, string cognome)
        {
            using var db = new UniContext();
            //Q7: Stampare i corsi seguiti dallo studente con Nome =“Piero” e Cognome =“Gallo”

            var corsiStud = db.Frequenze
                .Include(x => x.Studente)
                .Include(x => x.Corso)
                .Where(x => x.Studente.Nome == nome && x.Studente.Cognome == cognome);

            foreach (var item in corsiStud)
            {
                Console.WriteLine(item.Corso);
            }
        }

        static void InitTest()
        {
            UniContext db = new();
            //verifichiamo se il database esista già
            //https://medium.com/@Usurer/ef-core-check-if-db-exists-feafe6e36f4e
            //https://stackoverflow.com/questions/33911316/entity-framework-core-how-to-check-if-database-exists
            if (db.Database.GetService<IRelationalDatabaseCreator>().Exists())
            {
                WriteLineWithColor("Il database esiste già, vuoi ricrearlo da capo? Tutti i valori precedentemente inseriti verranno persi. [Si, No]", ConsoleColor.Red);
                bool dbErase = Console.ReadLine()?.StartsWith("si", StringComparison.CurrentCultureIgnoreCase) ?? false;
                if (dbErase)
                {
                    //cancelliamo il database se esiste
                    db.Database.EnsureDeleted();
                    //ricreiamo il database a partire dal model (senza dati --> tabelle vuote)
                    db.Database.EnsureCreated();
                    //inseriamo i dati nelle tabelle
                    PopulateDb();
                    Console.WriteLine("Database ricreato correttamente");
                }
            }
            else //il database non esiste
            {
                //ricreiamo il database a partire dal model (senza dati --> tabelle vuote)
                db.Database.EnsureCreated();
                //popoliamo il database
                PopulateDb();
                Console.WriteLine("Database creato correttamente");
            }

            static void PopulateDb()
            {
                //1) inserisco istanze nelle tabelle che non hanno chiavi esterne -->CorsoDiLaurea, Docente
                //creo una lista di CorsoDiLaurea e di Docente
                List<Docente> docenti =
                [
                    new (){CodDocente=1, Cognome="Malafronte", Nome="Gennaro",Dipartimento=Dipartimento.IngegneriaInformatica },
                    new (){CodDocente=2, Cognome="Rossi", Nome="Mario", Dipartimento=Dipartimento.Matematica},
                    new (){CodDocente=3, Cognome="Verdi", Nome="Giuseppe", Dipartimento=Dipartimento.Fisica},
                    new (){CodDocente=4, Cognome= "Smith", Nome="Albert", Dipartimento=Dipartimento.Economia}
                ];
                List<CorsoLaurea> corsiDiLaurea =
                [
                    new (){CorsoLaureaId = 1,TipoLaurea=TipoLaurea.Magistrale, Facoltà=Facoltà.Ingegneria},
                    new (){CorsoLaureaId = 2,TipoLaurea=TipoLaurea.Triennale, Facoltà=Facoltà.MatematicaFisicaScienze},
                    new (){CorsoLaureaId = 3,TipoLaurea=TipoLaurea.Magistrale, Facoltà=Facoltà.Economia},
                ];
                using (var db = new UniContext())
                {
                    docenti.ForEach(d => db.Add(d));
                    corsiDiLaurea.ForEach(cl => db.Add(cl));
                    db.SaveChanges();
                }
                //2) inserisco altre istanze: Inserisco istanze di Corso e di Studente
                List<Corso> corsi =
                [
                    new (){CodiceCorso=1,Nome="Fondamenti di Informatica 1", CodDocente=1},
                    new (){CodiceCorso=2,Nome="Analisi Matematica 1", CodDocente=2},
                    new (){CodiceCorso=3,Nome="Fisica 1", CodDocente=3},
                    new (){CodiceCorso=4, Nome="Microeconomia 1", CodDocente=4}
                ];
                List<Studente> studenti =
                [
                    new (){Matricola=1, Nome="Giovanni", Cognome="Casiraghi", CorsoLaureaId=1, AnnoNascita=2000},
                    new (){Matricola=2, Nome="Alberto", Cognome="Angela", CorsoLaureaId=2, AnnoNascita=1999},
                    new (){Matricola=3, Nome="Piero", Cognome="Gallo", CorsoLaureaId=3, AnnoNascita=2000}
                ];
                using (var db = new UniContext())
                {
                    corsi.ForEach(c => db.Add(c));
                    studenti.ForEach(s => db.Add(s));
                    db.SaveChanges();
                }
                //4) inserisco le frequenze - è la tabella molti a molti
                List<Frequenta> frequenze =
                [
                    new (){Matricola=1, CodCorso=1},// Giovanni Casiraghi frequenta il corso di Fondamenti di Informatica 1
                    new (){Matricola=1, CodCorso=2},// Giovanni Casiraghi frequenta il corso di Analisi Matematica 1
                    new (){Matricola=2, CodCorso=2},
                    new (){Matricola=2, CodCorso=3},
                    new (){Matricola=3, CodCorso=4}
                ];
                using (var db = new UniContext())
                {
                    frequenze.ForEach(f => db.Add(f));
                    db.SaveChanges();
                }

            }

        }

        static void WriteLineWithColor(string text, ConsoleColor consoleColor)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = previousColor;
        }
    }
}
