using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface IHistoricoSaudeRepository
	{
		Task<IEnumerable<HistoricoSaude>> ObterHistoricoPorCaoId(int caoId);
		Task AdicionarHistorico(HistoricoSaude historicoSaude);
		Task<bool> VerificarConsentimentoDono(int caoId);
		Task Atualizar(HistoricoSaude historicoSaude);
		Task<HistoricoSaude> ObterPorId(int id);
	}
}
