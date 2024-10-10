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

namespace ConexaoCaninaApp.Domain.Test.Services
{
	public class SugestaoServiceTests
	{
		private readonly Mock<ISugestaoRepository> _mockSugestaoRepository;
		private readonly SugestaoService _sugestaoService;

		public SugestaoServiceTests()
		{
			_mockSugestaoRepository = new Mock<ISugestaoRepository>();
			_sugestaoService = new SugestaoService(_mockSugestaoRepository.Object);
		}

		[Fact]
		public async Task ObterSugestoesPorUsuarioAsync_DeveRetornarSugestoesDoUsuario()
		{
			var usuarioId = 1;
			var sugestoes = new List<Sugestao>
			{
				new Sugestao { SugestaoId = 1, Descricao = "Sugestão 1", DataEnvio = DateTime.Now, Status = "Em Análise", UsuarioId = usuarioId },
				new Sugestao { SugestaoId = 2, Descricao = "Sugestão 2", DataEnvio = DateTime.Now, Status = "Aprovada", UsuarioId = usuarioId }
			};

			_mockSugestaoRepository.Setup(r => r.ObterSugestoesPorUsuarioAsync(usuarioId))
				.ReturnsAsync(sugestoes);

			var resultado = await _sugestaoService.ObterSugestoesPorUsuarioAsync(usuarioId);

			Assert.NotNull(resultado);
			Assert.Equal(2, resultado.Count);
			Assert.Equal("Sugestão 1", resultado[0].Descricao);
			Assert.Equal("Aprovada", resultado[1].Status);
			_mockSugestaoRepository.Verify(r => r.ObterSugestoesPorUsuarioAsync(usuarioId), Times.Once);
		}

		[Fact]
		public async Task EnviarSugestao_DeveAdicionarSugestaoComSucesso()
		{
			var sugestaoDto = new SugestaoDto { Descricao = "Sugestão de teste" };

			await _sugestaoService.EnviarSugestaoAsync(sugestaoDto);

			_mockSugestaoRepository.Verify(r => r.AdicionarAsync(It.IsAny<Sugestao>()), Times.Once);
		}
	}
}
