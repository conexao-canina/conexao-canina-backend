using ConexaoCaninaApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface  IHistoricoSaudeService
	{
		Task<IEnumerable<HistoricoSaudeDto>> ObterHistoricoSaudePorCaoId(int caoId);	
		Task AdicionarHistoricoSaude(HistoricoSaudeDto historicoSaudeDto);
		Task<bool> VerificarConsentimento(int caoId);	
	}
}
