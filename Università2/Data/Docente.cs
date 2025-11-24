using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Università2.Data
{
    public class Docente
    {
        [Key]
        public int CodDocente { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public Dipartimento Dipartimento { get; set; }

        public List<Corso> Corsi { get; set; } = null!;

        public override string ToString()
        {
            return $"CodDocente: {CodDocente}, Nome: {Nome}, Cognome: {Cognome}, Dipartimento: {Dipartimento}";
        }
    }
}
