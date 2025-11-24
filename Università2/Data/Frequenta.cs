using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Università2.Data
{
    public class Frequenta
    {
        [Key]
        public int Matricola { get; set; }
        [ForeignKey(nameof(Matricola))]
        public Studente Studente { get; set; } = null!;

        [Key]
        public int CodCorso { get; set; }
        [ForeignKey(nameof(CodCorso))]
        public Corso Corso { get; set; } = null!;

        public override string ToString()
        {
            return $"{Studente} frequenta {Corso}";
        }
    }
}
