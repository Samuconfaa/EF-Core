using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtilizziPC.Data
{
    internal class Computer
    {
        //Computer(Id, Modello, Collocazione )
        public int Id { get; set; }
        public string Modello { get; set; } = null!;
        public string Collocazione { get; set; } = null!;

        public List<Utilizza> Utilizzi { get; set; } = null!;
        public List<Studente> Studenti { get; set; } = null!;

        public override string ToString()
        {
            return $"{{{nameof(Id)} = {Id}, {nameof(Modello)} = {Modello}, {nameof(Collocazione)} = {Collocazione}}}";
        }

    }
}
