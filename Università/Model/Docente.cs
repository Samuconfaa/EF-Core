using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Università.Model
{
    public class Docente
    {
        [Key]
        public int CodDocente { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public Dipartimento Dipartimento { get; set; }

        public ICollection<Corso> Corsi { get; set; } = null!;

        public override string ToString()
        {
            return $"{CodDocente} - {Nome} {Cognome}, Dipartimento: {Dipartimento}";
        }

    }
}
