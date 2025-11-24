using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.Data
{
    public class Festival
    {
        //(Id, Nome, DataInizio)
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime DataInizio { get; set; }

        public List<Esibizione> Esibizioni { get; set; } = null!;
        public List<Cantante> Cantanti { get; set; } = null!;

        public override string ToString()
        {
            return $"Id {Id}, Nome: {Nome}, DataInizio: {DataInizio}";
        }
    }
}
