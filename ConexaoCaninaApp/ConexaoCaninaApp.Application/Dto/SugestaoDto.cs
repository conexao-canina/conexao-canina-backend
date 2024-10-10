using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class SugestaoDto
	{
		public int SugestaoId { get; set; }
		public string Descricao { get; set; }
		public DateTime DataEnvio { get; set; }
		public string Status { get; set; } = "Em Análise";
		public int UsuarioId { get; set; }


	}
}
