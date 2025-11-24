using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Università2.Data
{
    public class Corso
    {
        [Key]
        public int CodiceCorso { get; set; }
        public string Nome { get; set; } = null!;
        public int CodDocente { get; set; }
        [ForeignKey(nameof(CodDocente))]
        public Docente Docente { get; set; } = null!;

        public List<Frequenta> Frequenze { get; set; } = null!;
        public List<Studente> Studenti { get; set; } = null!;

        public override string ToString()
        {
            return $"CodCorso: {CodiceCorso}, Nome: {Nome}, CodDocente: {CodDocente}";
        }
    }
}
