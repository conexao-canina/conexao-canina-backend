using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
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
		private readonly Mock<INotificacaoService> _mockNotificacaoService;
		private readonly Mock<ICaoRepository> _mockCaoRepository;



		public SolicitacaoCruzamentoServiceTests()
		{
			_mockNotificacaoService = new Mock<INotificacaoService>();
			_mockSolicitacaoRepository = new Mock<ISolicitacaoCruzamentoRepository>();
			_mockCaoRepository = new Mock<ICaoRepository>();

			_solicitacaoService = new SolicitacaoCruzamentoService(
				_mockSolicitacaoRepository.Object,
				_mockNotificacaoService.Object,
				_mockCaoRepository.Object);
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

			var cao = new Cao 
			{
				Nome = "Rex",
				Proprietario = new Proprietario
				{
					Email = "proprietario@teste.com"
				}
			};

			
			_mockCaoRepository.Setup(r => r.ObterPorId(solicitacaoDto.CaoId)).ReturnsAsync(cao);

			_mockSolicitacaoRepository
				.Setup(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()))
				.Returns(Task.CompletedTask);

			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);

			_mockSolicitacaoRepository.Verify(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()), Times.Once);
			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoSolicitacaoCruzamento(cao.Proprietario.Email, cao.Nome, solicitacaoDto.Mensagem), Times.Once);
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
			var cao = new Cao
			{
				CaoId = 1,
				Nome = "Cão Teste",
				Proprietario = new Proprietario { Email = "dono@teste.com" }
			};

			_mockSolicitacaoRepository
				.Setup(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()))
				.Returns(Task.CompletedTask);

			_mockCaoRepository
				.Setup(r => r.ObterPorId(solicitacaoDto.CaoId))
				.ReturnsAsync(cao);
				



			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);


			_mockSolicitacaoRepository.Verify(r => r.Adicionar(It.IsAny<SolicitacaoCruzamento>()), Times.Once);
			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoSolicitacaoCruzamento(cao.Proprietario.Email, cao.Nome, solicitacaoDto.Mensagem), Times.Once);

		}

		[Fact]
		public async Task EnviarSolicitacao_Deve_EnviarNotificacaoParaUsuario()
		{
			var solicitacaoDto = new SolicitacaoCruzamentoDto
			{
				UsuarioId = 1,
				CaoId = 1,
				Mensagem = "Gostaria de cruzar os cães"
			};

			
			var cao = new Cao
			{
				CaoId = 1,
				Nome = "PHG",
				Proprietario = new Proprietario
				{
					Email = "dono@teste.com"
				}
			};

			_mockCaoRepository
				.Setup(r => r.ObterPorId(solicitacaoDto.CaoId))
				.ReturnsAsync(cao);

			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);

			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoSolicitacaoCruzamento(
				cao.Proprietario.Email,
				cao.Nome,
				solicitacaoDto.Mensagem), Times.Once);
		}

	}
}
