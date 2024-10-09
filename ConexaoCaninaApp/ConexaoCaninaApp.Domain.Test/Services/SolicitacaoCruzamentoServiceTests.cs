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
	public class SolicitacaoCruzamentoServiceTests
	{
		private readonly SolicitacaoCruzamentoService _solicitacaoService;
		private readonly Mock<ISolicitacaoCruzamentoRepository> _mockSolicitacaoRepository;


		public SolicitacaoCruzamentoServiceTests()
		{
			_mockSolicitacaoRepository = new Mock<ISolicitacaoCruzamentoRepository>();
			_solicitacaoService = new SolicitacaoCruzamentoService(_mockSolicitacaoRepository.Object);
		}


		[Fact]
		public async Task EnviarSolicitacao_Deve_SalvarSolicitacaoNoBanco()
		{
			var solicitacaoDto = new SolicitacaoCruzamentoDto
			{
				UsuarioId = 1,
				CaoId = 1,
				Mensagem = "Gostaria de levar meu super cachorro ao encontro do cachorro de seu marido"
			};

			_mockSolicitacaoRepository
				.Setup(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()))
				.Returns(Task.CompletedTask);


			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);


			_mockSolicitacaoRepository .Verify(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()), Times.Once);

		}

		[Fact]
		public async Task EnviarSolicitacao_ComMensagemVazia_DeveSalvarMesmoAssim()
		{
			var solicitacaoDto = new SolicitacaoCruzamentoDto
			{
				UsuarioId = 1,
				CaoId = 1,
				Mensagem = string.Empty
			};

			_mockSolicitacaoRepository
				.Setup(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()))
				.Returns(Task.CompletedTask);

			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);


			_mockSolicitacaoRepository.Verify(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()), Times.Once);
		}
	}
}
