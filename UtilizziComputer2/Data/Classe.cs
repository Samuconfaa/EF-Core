using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtilizziPC.Data
{
    [Index(nameof(Nome), IsUnique = true)]
    internal class Classe
    {
        //id nome aula
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Aula { get; set; } = null!;

        public List<Studente> Studenti { get; set; } = null!;

        public override string ToString()
        {
            return $"{{{nameof(Id)} = {Id}, {nameof(Nome)} = {Nome}, {nameof(Aula)} = {Aula}}}";
        }
    }
}
