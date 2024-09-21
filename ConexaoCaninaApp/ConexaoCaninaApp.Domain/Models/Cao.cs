using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
    public class Cao
    {
        public int CaoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Idade { get; set; }
        public string Raca { get; set; }
        public int Genero { get; set; } // 1-M | 2-F
        public int ProprietarioId { get; set; }
    }
}
