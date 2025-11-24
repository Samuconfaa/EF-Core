using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.Data
{
    public class Etichetta
    {
        //(Id, Nome, SedeLegale
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string SedeLegale { get; set; } = null!;

        public List<Cantante> Cantanti { get; set; } = null!;

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, SedeLegale: {SedeLegale}";
        }
    }
}
