using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilizziComputer.Data
{
    [PrimaryKey(nameof(StudenteId), nameof(ComputerId), nameof(DataOraInizioUtilizzo))]
    internal class Utilizza
    {
        public int StudenteId { get; set; }
        public Studente Studente { get; set; } = null!;
        public int ComputerId { get; set; } 
        public Computer Computer { get; set; } = null!;
        public DateTime DataOraInizioUtilizzo { get; set; } 
        public DateTime? DataOraFineUtilizzo { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(StudenteId)} = {StudenteId}, {nameof(ComputerId)} = {ComputerId}," +
                $" {nameof(DataOraInizioUtilizzo)} = {DataOraInizioUtilizzo}," +
                $" {nameof(DataOraFineUtilizzo)} = {DataOraFineUtilizzo}}}";
        }
    }
}
