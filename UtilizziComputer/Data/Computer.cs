using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilizziComputer.Data
{
    internal class Computer
    {
        public int Id { get; set; }
        public string Modello { get; set; }
        public string Collocazione { get; set; }
        public List<Studente> Studenti { get; set; } = null!;
        public List<Utilizza> Utilizzi { get; set; } = null!;

        public override string ToString()
        {
            return $"{{{nameof(Id)} = {Id}, {nameof(Modello)} = {Modello}, {nameof(Collocazione)} = {Collocazione}}}";
        }
    }
}
