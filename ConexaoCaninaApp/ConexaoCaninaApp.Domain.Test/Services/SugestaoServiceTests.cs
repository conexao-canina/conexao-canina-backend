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
		public async Task EnviarSugestao_DeveAdicionarSugestaoComSucesso()
		{
			var sugestaoDto = new SugestaoDto { Descricao = "Sugestão de teste" };

			await _sugestaoService.EnviarSugestaoAsync(sugestaoDto);

			_mockSugestaoRepository.Verify(r => r.AdicionarAsync(It.IsAny<Sugestao>()), Times.Once);
		}
	}
}
