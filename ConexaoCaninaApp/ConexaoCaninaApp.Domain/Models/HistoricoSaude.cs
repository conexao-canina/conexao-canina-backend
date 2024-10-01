using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class HistoricoSaude
	{
		public int HistoricoSaudeId { get; set; }
		public string Exame { get; set; }
		public string Vacinas { get; set; }
		public string CondicoesDeSaude { get; set; }
		public DateTime Data { get; set; }

		public int CaoId { get; set; }
		public Cao Cao { get; set; }

	}
}
