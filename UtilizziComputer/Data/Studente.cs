using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilizziComputer.Data
{
    internal class Studente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public int ClasseId { get; set; }
        public Classe Classe { get; set; } = null!;
        public List<Utilizza> Utilizzi {  get; set; } = null!;
        public List<Computer> Computers { get; set; } = null!;

        public override string ToString()
        {
            return $"{{{nameof(Id)} = {Id}, {nameof(Nome)} = {Nome}, {nameof(Cognome)} = {Cognome}, {nameof(Classe)} = {Classe}}}";
        }
    }
}
