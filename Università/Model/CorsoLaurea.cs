using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Università.Model
{
    public class CorsoLaurea
    {
        public int CorsoLaureaId { get; set; }
        public TipoLaurea TipoLaurea { get; set; }
        public Facoltà Facoltà { get; set; }

        public ICollection<Studente> Studenti { get; set; } = null!;

        public override string ToString()
        {
            return $"ID: {CorsoLaureaId} - {TipoLaurea}, Facoltà: {Facoltà}";
        }

    }
}
