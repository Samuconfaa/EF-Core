using System;
using System.Collections.Generic;
using System.Text;

namespace Università2.Data
{
    public class CorsoLaurea
    {
        public int CorsoLaureaId { get; set; }
        public TipoLaurea TipoLaurea { get; set; }
        public Facoltà Facoltà { get; set; }

        public List<Studente> Studenti { get; set; } = null!;

        public override string ToString()
        {
            return $"CorsoLaureaId: {CorsoLaureaId}, TipoLaurea: {TipoLaurea}, Facoltà: {Facoltà}";
        }
    }
}
