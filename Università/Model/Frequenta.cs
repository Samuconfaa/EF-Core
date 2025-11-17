using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Università.Model
{
    public class Frequenta
    {
        public int Matricola { get; set; }
        public Studente Studente { get; set; } = null!;
        public int CodCorso { get; set; }
        public Corso Corso { get; set; } = null!;

        public override string ToString()
        {
            return $"Studente {Matricola} → Corso {CodCorso}";
        }


    }
}
