using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Università.Model
{
    public class Corso
    {
        [Key]
        public int CodiceCorso { get; set; }
        public string Nome { get; set; } = null!;
        public int CodDocente { get; set; }
        [ForeignKey("CodDocente")]
        public Docente Docente { get; set; } = null!;

        public ICollection<Studente> Studenti { get; set; } = null!;
        public ICollection<Frequenta> Frequenze { get; set; } = null!;

        public override string ToString()
        {
            return $"Corso {CodiceCorso}: {Nome}";
        }

    }
}
