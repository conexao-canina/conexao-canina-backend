using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConexaoCaninaApp.Domain.Test
{
	public class HistoricoSaudeServiceTests
	{
		private readonly Mock<IHistoricoSaudeRepository> _mockHistoricoSaudeRepository;
		private readonly HistoricoSaudeService _historicoSaudeService;

		public HistoricoSaudeServiceTests()
		{
			_mockHistoricoSaudeRepository = new Mock<IHistoricoSaudeRepository>();
			_historicoSaudeService = new HistoricoSaudeService(_mockHistoricoSaudeRepository.Object);
		}

		[Fact]
		public async Task AdicionarHistoricoSaude_Deve_Adicionar_Novo_Historico()
		{
			var novoHistoricoDto = new HistoricoSaudeDto
			{
				CaoId = 1,
				Exame = "Exames gerais",
				Vacinas = "Vacina da raiva",
				CondicoesDeSaude = "Nenhuma"
			};

			await _historicoSaudeService.AdicionarHistoricoSaude(novoHistoricoDto);

			_mockHistoricoSaudeRepository.Verify(r => r.AdicionarHistorico(It.IsAny<HistoricoSaude>()), Times.Once);
		}

		[Fact]
		public async Task ObterHistoricoSaude_Deve_Retornar_Historico()
		{
			var caoId = 1;
			var historicoEsperado = new List<HistoricoSaude>
			{
				new HistoricoSaude
				{
					CaoId = caoId,
					Exame = "Exame1",
					Vacinas = "Vacina1"
				}
			};

			_mockHistoricoSaudeRepository.Setup(r => r.ObterHistoricoPorCaoId
			(caoId)).ReturnsAsync(historicoEsperado);

			var result = await _historicoSaudeService.ObterHistoricoSaudePorCaoId(caoId);

			Assert.NotNull(result);	
			Assert.Equal(1, result.Count());
		}
	}
}
