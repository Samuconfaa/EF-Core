using System;
using System.Collections.Generic;
using System.Text;

namespace UtilizziComputer3.Data
{
    public class Computer
    {
        public int Id { get; set; }
        public string Modello { get; set; } = null!;
        public string Collocazione { get; set; } = null!;

        public List<Utilizza> Utilizzi { get; set; } = null!;
        public List<Studente> Studenti { get; set; } = null!;


        public override string ToString()
        {
            return $"Id: {Id}, Modello: {Modello}, Collocazione: {Collocazione}";
        }
    }
}
