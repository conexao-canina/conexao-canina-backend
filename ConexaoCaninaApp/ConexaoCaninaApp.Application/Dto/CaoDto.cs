using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class CaoDto
	{
		public string Nome { get; set; }
		public string Raca { get; set; }
		public int Idade { get; set; }
		public string Descricao { get; set; }
		public int Genero { get; set; } // 1-M, 2-F
		public TamanhoCao Tamanho { get; set; }
		public string CaracteristicasUnicas { get; set; }
		public int ProprietarioId { get; set; }
	}
}
