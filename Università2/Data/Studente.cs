using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using System.Text;

namespace Università2.Data
{
    public class Studente
    {
        [Key]
        public int Matricola { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public int CorsoLaureaId { get; set; }
        public CorsoLaurea CorsoLaurea { get; set; } = null!;
        public int AnnoNascita { get; set; }

        public List<Frequenta> Frequenze { get; set; } = null!;
        public List<Corso> Corsi { get; set; } = null!;

        public override string ToString()
        {
            return $"Matricola: {Matricola}, Nome: {Nome}, Cognome: {Cognome}, Anno Nascita: {AnnoNascita}";
        }

    }
}
