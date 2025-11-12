using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilizziComputer.Data
{
    internal class Classe
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Aula { get; set; } = null!;
        public List<Studente> Studenti { get; set;} = null!;

        public override string ToString()
        {
            return $"{{{nameof(Id)} = {Id}, {nameof(Nome)} = {Nome}, {nameof(Aula)} = {Aula}}}";
        }
    }
}
