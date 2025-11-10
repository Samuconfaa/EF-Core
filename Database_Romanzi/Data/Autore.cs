using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Romanzi.Data
{
    internal class Autore
    {
        //Autore(AutoreId, Nome, Cognome, Nazionalità )
        //Romanzo(RomanzoId, Titolo, AutoreId*, AnnoPubblicazione )
        //Personaggio(PersonaggioId, Nome, RomanzoId*, Sesso, Ruolo )

        public int AutoreId { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public string Nazionalita { get; set; } = null!;
    }
}
