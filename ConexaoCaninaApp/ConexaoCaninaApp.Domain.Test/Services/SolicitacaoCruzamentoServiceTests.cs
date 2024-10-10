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
using static ConexaoCaninaApp.Domain.Models.SolicitacaoCruzamento;

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



		[Fact]
		public async Task AceitarSolicitacao_DeveAtualizarStatusParaAceita()
		{
			var solicitacaoId = 1;
			var solicitacao = new SolicitacaoCruzamento
			{
				SolicitacaoId = solicitacaoId,
				UsuarioId = solicitacaoId,
				CaoId = 1,
				Status = StatusSolicitacao.Pendente
			};

			_mockSolicitacaoRepository
			  .Setup(r => r.ObterPorId(solicitacaoId))
			  .ReturnsAsync(solicitacao);


			await _solicitacaoService.AceitarSolicitacaoAsync(solicitacaoId);


			Assert.Equal(StatusSolicitacao.Aceita, solicitacao.Status);
			_mockSolicitacaoRepository.Verify(r => r.Atualizar(solicitacao), Times.Once);

		}

		[Fact]
		public async Task RejeitarSolicitacao_DeveAtualizarStatusParaRejeitada()
		{
			var solicitacaoId = 1;
			var solicitacao = new SolicitacaoCruzamento
			{
				SolicitacaoId = solicitacaoId,
				UsuarioId = 1,
				CaoId = 1,
				Usuario = new Usuario
				{
					Email = "usuario@exemplo.com"
				},
				Cao = new Cao
				{
					Nome = "PHG"
				},
				Status = StatusSolicitacao.Pendente
			};

			_mockSolicitacaoRepository.Setup(r => r.ObterPorId(solicitacaoId)).ReturnsAsync(solicitacao);

			await _solicitacaoService.RejeitarSolicitacaoAsync(solicitacaoId);

			Assert.Equal(StatusSolicitacao.Rejeitada, solicitacao.Status);
			_mockSolicitacaoRepository.Verify(r => r.Atualizar(solicitacao), Times.Once);
		}

		[Fact]
		public async Task RejeitarSolicitacao_DeveDispararNotificacaoRejeicao()
		{
			// Arrange
			var solicitacaoId = 1;
			var solicitacao = new SolicitacaoCruzamento
			{
				SolicitacaoId = solicitacaoId,
				UsuarioId = 1,
				CaoId = 1,
				Usuario = new Usuario
				{
					Email = "usuario@exemplo.com"
				},
				Cao = new Cao
				{
					Nome = "JVP"
				},
				Status = StatusSolicitacao.Pendente
			};

			_mockSolicitacaoRepository.Setup(r => r.ObterPorId(solicitacaoId))
				.ReturnsAsync(solicitacao); 

			var requisitosNaoAtendidos = "Exemplo: Temperamento não compatível, Tamanho não adequado";

			// Act
			await _solicitacaoService.RejeitarSolicitacaoAsync(solicitacaoId);

			
			_mockNotificacaoService.Verify(n =>
				n.EnviarNotificacaoSolicitacaoRejeitada(
					solicitacao.Usuario.Email,
					solicitacao.Cao.Nome,
					It.Is<string>(msg => msg.Contains(requisitosNaoAtendidos))
				), Times.Once);

			
			_mockSolicitacaoRepository.Verify(r => r.Atualizar(solicitacao), Times.Once);
		}

		[Fact]
		public async Task AceitarSolicitacao_SolicitacaoNaoEncontrada_DeveLancarExcecao()
		{
			var solicitacaoId = 1;

			_mockSolicitacaoRepository
				.Setup(r => r.ObterPorId(solicitacaoId))
				.ReturnsAsync((SolicitacaoCruzamento)null);


			await Assert.ThrowsAsync<Exception>(() =>
			_solicitacaoService.AceitarSolicitacaoAsync(solicitacaoId));

		}

		[Fact]
		public async Task AceitarSolicitacao_ErroAoAtualizar_DeveLancarExcecao()
		{
			var solicitacaoId = 1;
			var solicitacao = new SolicitacaoCruzamento
			{
				SolicitacaoId = solicitacaoId,
				UsuarioId = solicitacaoId,
				CaoId = 1,
				Status = StatusSolicitacao.Pendente
			};

			_mockSolicitacaoRepository
			  .Setup(r => r.ObterPorId(solicitacaoId))
			  .ReturnsAsync(solicitacao);

			_mockSolicitacaoRepository
				.Setup(r => r.Atualizar(solicitacao))
				.ThrowsAsync(new Exception("Erro ao atualizar"));

			await Assert.ThrowsAsync<Exception>(() =>
			_solicitacaoService.AceitarSolicitacaoAsync(solicitacaoId));


		}
		[Fact]
		public async Task ValidarSolicitacao_DeveRetornarVerdadeiroSeAtenderRequisitos()
		{
			var solicitacaoDto = new SolicitacaoCruzamentoDto
			{
				CaoId = 1,
				Cao = new Cao
				{
					RequisitosCruzamento = new RequisitosCruzamento
					{
						Temperamento = "Calmo",
						Tamanho = "Grande",
						CaracteristicasGeneticas = "Nenhum problema"
					}
				},
				Mensagem = "Solicitação de cruzamento.",
				UsuarioId = 1
			};
			var cao = new Cao
			{
				CaoId = 1,
				RequisitosCruzamento = new RequisitosCruzamento
				{
					Temperamento = "Calmo",
					Tamanho = "Grande",
					CaracteristicasGeneticas = "Nenhum problema"
				}
			};


			_mockCaoRepository.Setup(r => r.ObterPorId(solicitacaoDto.CaoId)).ReturnsAsync(cao);
			var resultado = await _solicitacaoService.ValidarSolicitacaoAsync(solicitacaoDto);
			Assert.True(resultado);
			_mockCaoRepository.Verify(r => r.ObterPorId(solicitacaoDto.CaoId), Times.Once);
		}


		[Fact]
		public async Task ValidarSolicitacao_DeveRetornarFalsoSeNaoAtenderRequisitos()
		{
			var solicitacaoDto = new SolicitacaoCruzamentoDto
			{
				CaoId = 1,
				Cao = new Cao
				{
					RequisitosCruzamento = new RequisitosCruzamento
					{
						Temperamento = "Agitado",
						Tamanho = "Pequeno",
						CaracteristicasGeneticas = "Problema de saúde genético"
					}
				},
				Mensagem = "Solicitação de cruzamento",
				UsuarioId = 1
			};

			var cao = new Cao
			{
				RequisitosCruzamento = new RequisitosCruzamento
				{
					Temperamento = "Calmo",
					Tamanho = "Grande",
					CaracteristicasGeneticas = "Nenhum problema"
				}
			};

			_mockCaoRepository.Setup(r => r.ObterPorId(solicitacaoDto.CaoId)).ReturnsAsync(cao);

			var resultado = await _solicitacaoService.ValidarSolicitacaoAsync(solicitacaoDto);

			Assert.False(resultado);
			_mockCaoRepository.Verify(r => r.ObterPorId(solicitacaoDto.CaoId), Times.Once);

		}
	}
}
