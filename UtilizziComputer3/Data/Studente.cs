using System;
using System.Collections.Generic;
using System.Text;

namespace UtilizziComputer3.Data
{
    public class Studente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public int ClasseId { get; set; }
        public Classe Classe { get; set; } = null!;

        public List<Utilizza> Utilizzi { get; set; } = null!;
        public List<Computer> Computer { get; set; } = null!;


        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, Cognome: {Cognome}";
        }
    }

}
