using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class HistoricoDeSaudeDto
	{
		public Guid HistoricoSaudeId { get; set; }
		public string Exame { get; set; }
		public string Vacina { get; set; }
		public string CondicoesDeSaude { get; set; }
		public bool ConsentimentoDono { get; set; }
		public DateTime DateExame { get; set; }
	}
}
