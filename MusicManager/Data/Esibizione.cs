using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.Data
{
    public class Esibizione
    {
        //(FestivalId*, CantanteId*, VotiGiuria, OrdineUscita)
        public int FestivalId { get; set; }
        public Festival Festival { get; set; } = null!;

        public int CantanteId { get; set; }
        public Cantante Cantante { get; set; } = null!;

        public int VotiGiuria { get; set; }
        public int OrdineUscita { get; set; }

        public override string ToString()
        {
            return $"Il cantante {Cantante} si è esibito al fetival {Festival} come numero {OrdineUscita} e ha ottenuto {VotiGiuria} punti";
        }
    }
}
