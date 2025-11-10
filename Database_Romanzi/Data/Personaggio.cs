using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Romanzi.Data
{
    internal class Personaggio
    {
        //Autore(AutoreId, Nome, Cognome, Nazionalità )
        //Romanzo(RomanzoId, Titolo, AutoreId*, AnnoPubblicazione )
        //Personaggio(PersonaggioId, Nome, RomanzoId*, Sesso, Ruolo )

        public int PersonaggioId { get; set; }
        public string Nome { get; set; } = null!;
        public string Sesso { get; set; } = null!;
        public string Ruolo { get; set; } = null!;
        public int RomanzoId { get; set; }
        public Romanzo Romanzo { get; set; } = null!;
    }
}
