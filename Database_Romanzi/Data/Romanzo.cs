using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Romanzi.Data
{
    internal class Romanzo
    {
        //Autore(AutoreId, Nome, Cognome, Nazionalità )
        //Romanzo(RomanzoId, Titolo, AutoreId*, AnnoPubblicazione )
        //Personaggio(PersonaggioId, Nome, RomanzoId*, Sesso, Ruolo )

        public int RomanzoId { get; set; }
        public string Titolo { get; set; } = null!;
        public int AutoreId { get; set; }
        public Autore Autore { get; set; } = null!;
        public int AnnoPubblicazione { get; set; }
    }
}
