using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class AtualizarHistoricoSaudeDto
	{
		public int HistoricoSaudeId { get; set; }
		public string Exame { get; set; }
		public string Vacinas { get; set; }
		public string CondicoesDeSaude { get; set; }
		public DateTime Data { get; set; }
	}
}
