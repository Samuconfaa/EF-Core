using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilizziComputer3.Data
{
    [Index(nameof(Nome), IsUnique = true)]
    public class Classe
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Aula { get; set; } = null!;

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, Aula: {Aula}";
        }
    }
}
