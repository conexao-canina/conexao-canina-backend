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

namespace ConexaoCaninaApp.Domain.Test
{
	public class CaoServiceTests
	{
		private readonly CaoService _caoService;
		private readonly Mock<ICaoRepository> _mockCaoRepository;
		private readonly Mock<INotificacaoService> _mockNotificacaoService;

		public CaoServiceTests()
		{
			_mockCaoRepository = new Mock<ICaoRepository>();
			_mockNotificacaoService = new Mock<INotificacaoService>();
			_caoService = new CaoService(_mockCaoRepository.Object, _mockNotificacaoService.Object);
		}

		[Fact]
		public async Task ObterPorId_Deve_Retornar_Cao_Quando_Existe()
		{
			// ARRANGE

			var caoId = 1;
			var expectedCao = new Cao
			{
				CaoId = caoId,
				Nome = "Deus Imperador PHG"
			};

			_mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
			.ReturnsAsync(expectedCao);

			// ACT

			var result = await _caoService.ObterPorId(caoId);

			// ASSERT

			Assert.NotNull(result);
			Assert.Equal(caoId, result.CaoId);
			Assert.Equal("Deus Imperador PHG", result.Nome);
		}

		[Fact]
		public async Task ObterPorId_Deve_Retornar_Null_Quando_Cao_Nao_Existe()
		{
			// ARRANGE

			var caoId = 1;

			_mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
		   .ReturnsAsync((Cao)null);

			// ACT

			var result = await _caoService.ObterPorId(caoId);

			// ASSERT

			Assert.Null(result);
		}

		[Fact]
		public async Task AdicionarCao_Deve_Chamar_Repositorio_E_Notificar_Administrador()
		{
			// ARRANGE

			var caoDto = new CaoDto
			{
				Nome = "Deus Imperador PHG",
				Raca = "Pastor Alemão",
				Idade = 20,
				Tamanho = TamanhoCao.Grande,
				ProprietarioId = 1
			};

			var novoCao = new Cao
			{
				Nome = caoDto.Nome,
				Raca = caoDto.Raca,
				Idade = caoDto.Idade,
				Genero = caoDto.Genero,
				Tamanho = caoDto.Tamanho,
				ProprietarioId = caoDto.ProprietarioId,
				Status = StatusCao.Pendente
			};

			// ACT

			var result = await _caoService.AdicionarCao(caoDto);

			// ASSERT

			_mockCaoRepository.Verify(repo => repo.Adicionar(It.IsAny<Cao>()), Times.Once);
			_mockNotificacaoService.Verify(notif => notif.EnviarNotificacaoParaAdministrador(It.IsAny<Cao>()), Times.Once);
			Assert.NotNull(result);
			Assert.Equal(StatusCao.Pendente, result.Status);
		}

		[Fact]
		public async Task AprovarCao_Deve_Atualizar_Status_Para_Aprovado_E_Notificar_Usuario()
		{
			// ARRANGE

			var caoId = 1;
			var caoExistente = new Cao
			{
				CaoId = caoId,
				Nome = "Deus Imperador PHG",
				Status = StatusCao.Pendente
			};

			_mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
		   .ReturnsAsync(caoExistente);

			// ACT

			await _caoService.AprovarCao(caoId);

			// ASSERT

			_mockCaoRepository.Verify(repo => repo.Atualizar(It.IsAny<Cao>()), Times.Once);
			_mockNotificacaoService.Verify(notif => notif.EnviarNotificacaoParaUsuario(It.IsAny<Cao>()), Times.Once);
			Assert.Equal(StatusCao.Aprovado, caoExistente.Status);
		}

		[Fact]
		public async Task EditarPerfil_Deve_Definir_Status_Pendente_E_Notificar_Administrador()
		{
			// ARRANGE
			var editarCaoDto = new EditarCaoDto
			{
				CaoId = 1,
				Idade = 5,
				Descricao = "Lindo, forte e adoravel",
				CaracteristicasUnicas = "Forte"
			};

			var caoExistente = new Cao
			{
				CaoId = 1,
				Nome = "General PHG",
				Idade = 3,
				Status = StatusCao.Aprovado
			};

			_mockCaoRepository.Setup(repo => repo.ObterPorId(editarCaoDto.CaoId))
				.ReturnsAsync(caoExistente);

			// ACT

			await _caoService.AtualizarCao(editarCaoDto);

			// ASSERT
			_mockCaoRepository.Verify(repo => repo.Atualizar
			(It.Is<Cao>(c => c.Status == StatusCao.Pendente)), Times.Once);
			_mockNotificacaoService.Verify(serv => serv.EnviarNotificacaoParaAdministrador
			(It.IsAny<Cao>()), Times.Once);

		}
	}
}

