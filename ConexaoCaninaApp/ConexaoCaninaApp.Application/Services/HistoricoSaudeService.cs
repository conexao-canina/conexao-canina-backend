using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class HistoricoSaudeService : IHistoricoSaudeService
	{
		private readonly IHistoricoSaudeRepository _historicoSaudeRepository;

		public HistoricoSaudeService(IHistoricoSaudeRepository historicoSaudeRepository)
		{
			_historicoSaudeRepository = historicoSaudeRepository;
		}

		public async Task<IEnumerable<HistoricoSaudeDto>> ObterHistoricoSaudePorCaoId(int caoId)
		{
			var historicos = await _historicoSaudeRepository.ObterHistoricoPorCaoId(caoId);

			return historicos.Select(h => new HistoricoSaudeDto
			{
				CaoId = h.CaoId,
				Exame = h.Exame,
				Vacinas = h.Vacinas,
				CondicoesDeSaude = h.CondicoesDeSaude,
				Data = h.Data
			});
		}

		public async Task AdicionarHistoricoSaude(HistoricoSaudeDto historicoSaudeDto)
		{
			var historico = new HistoricoSaude
			{
				CaoId = historicoSaudeDto.CaoId,
				Exame = historicoSaudeDto.Exame,
				Vacinas = historicoSaudeDto.Vacinas,
				CondicoesDeSaude = historicoSaudeDto.CondicoesDeSaude,
				Data = historicoSaudeDto.Data
			};

			await _historicoSaudeRepository.AdicionarHistorico(historico);
		}
	}
}
