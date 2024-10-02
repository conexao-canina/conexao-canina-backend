using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConexaoCaninaApp.Domain.Test.Services
{
    public class HistoricoSaudeServiceTests : IClassFixture<WebApplicationFactory<Program>>
	{
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IHistoricoSaudeRepository> _mockHistoricoSaudeRepository;
        private readonly HistoricoSaudeService _historicoSaudeService;

		public HistoricoSaudeServiceTests(WebApplicationFactory<Program> factory)
		{
            _factory = factory;
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
        public async Task ObterHistoricoSaude_Deve_Retornar_Historico_Se_Consentimento_Existe()
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

            _mockHistoricoSaudeRepository.Setup(r => r.VerificarConsentimentoDono(caoId))
                .ReturnsAsync(true);

			_mockHistoricoSaudeRepository.Setup(r => r.ObterHistoricoPorCaoId
            (caoId)).ReturnsAsync(historicoEsperado);

            var result = await _historicoSaudeService.ObterHistoricoSaudePorCaoId(caoId);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }	

        [Fact]
        public async Task ObterHistoricoSaude_Deve_Retornar_Forbid_Se_Consentimento_Nao_Existe()
        {
            // ARRANGE

            var caoId = 1;

            _mockHistoricoSaudeRepository.Setup(r => r.VerificarConsentimentoDono(caoId))
                .ReturnsAsync(false);

            // ACT & ASSERT 

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
            {
                await _historicoSaudeService.ObterHistoricoSaudePorCaoId(caoId);
            });

		}
	}
}
