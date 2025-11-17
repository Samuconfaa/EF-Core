using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Università.Model
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

        public ICollection<Corso> Corsi { get; set; } = null!;
        public ICollection<Frequenta> Frequenze { get; set; } = null!;

        public override string ToString()
        {
            return $"{Matricola} - {Nome} {Cognome}, Anno nascita: {AnnoNascita}";
        }
    }
}
