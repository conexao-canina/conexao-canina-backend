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
			var consentimentoDono = await _historicoSaudeRepository.VerificarConsentimentoDono(caoId);

			if (!consentimentoDono)
			{
				throw new UnauthorizedAccessException("O dono nao consentiu com o fornecimento");
			}

			var historico = await _historicoSaudeRepository.ObterHistoricoPorCaoId(caoId);

			return historico.Select(h => new HistoricoSaudeDto
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
				Data = historicoSaudeDto.Data,
				ConsentimentoDono = true
				
			};

			await _historicoSaudeRepository.AdicionarHistorico(historico);
		}

		public async Task<bool> VerificarConsentimento(int caoId)
		{
			return await _historicoSaudeRepository.VerificarConsentimentoDono(caoId);
		}

		public async Task AtualizarHistoricoSaude(AtualizarHistoricoSaudeDto atualizarHistoricoSaudeDto)
		{
			var historico = await _historicoSaudeRepository.ObterPorId(atualizarHistoricoSaudeDto.HistoricoSaudeId);

			if (historico == null)
			{
				throw new Exception("Histórico de saúde não encontrado");
			}

			historico.Exame = atualizarHistoricoSaudeDto.Exame;
			historico.Vacinas = atualizarHistoricoSaudeDto.Vacinas;
			historico.CondicoesDeSaude = atualizarHistoricoSaudeDto.CondicoesDeSaude;
			historico.Data = atualizarHistoricoSaudeDto.Data;

			await _historicoSaudeRepository.Atualizar(historico);

		}
	}
}
