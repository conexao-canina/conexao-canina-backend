using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class HistoricoDeSaude
	{
		private HistoricoDeSaude() { }

		public HistoricoDeSaude(string exame, string vacina, 
			string condicoesDeSaude, bool consentimentoDono,
			DateTime dataExame)
		{
			HistoricoSaudeId = Guid.NewGuid();
			Exame = exame;
			Vacina = vacina;
			CondicoesDeSaude = condicoesDeSaude;
			ConsentimentoDono = consentimentoDono;
			DataExame = dataExame;

		}

		public Guid HistoricoSaudeId { get; set; }
		public DateTime DataExame { get; set; }
		public string Exame { get; set; }
		public string Vacina { get; set; }
		public string CondicoesDeSaude { get; set; }
		public bool ConsentimentoDono { get; set; }
	}
}
