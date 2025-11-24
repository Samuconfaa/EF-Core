using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.Data
{
    public class Cantante
    {
        //(Id, NomeArte, NomeReale, EtichettaId*)
        public int Id { get; set; }
        public string NomeArte { get; set; } = null!;
        public string NomeReale { get; set; } = null!;
        public int EtichettaId { get; set; }
        public Etichetta Etichetta { get; set; } = null!;

        public List<Esibizione> Esibizioni { get; set; } = null!;
        public List<Festival> Festival { get; set; } = null!;

        public override string ToString()
        {
            return $"Id: {Id}, NomeArte: {NomeArte}, NomeReale: {NomeReale}";
        }
    }
}
