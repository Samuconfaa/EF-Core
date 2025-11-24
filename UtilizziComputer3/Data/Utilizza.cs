using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilizziComputer3.Data
{
    [PrimaryKey(nameof(StudenteId), nameof(ComputerId), nameof(DataOraInizioUtilizzo))]
    public class Utilizza
    {
        public int StudenteId { get; set; }
        public Studente Studente { get; set; } = null!;

        public int ComputerId { get; set; }
        public Computer Computer { get; set; } = null!;

        public DateTime DataOraInizioUtilizzo { get; set; }
        public DateTime? DataOraFineUtilizzo { get; set; }


        public override string ToString()
        {
            return $"Studente: {Studente}, Computer: {Computer}, Inizio: {DataOraInizioUtilizzo}, Fine: {DataOraFineUtilizzo}";
        }
    }
}
