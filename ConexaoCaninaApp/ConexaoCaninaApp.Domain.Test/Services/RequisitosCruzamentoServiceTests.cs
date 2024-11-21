//using ConexaoCaninaApp.Application.Dto;
//using ConexaoCaninaApp.Application.Interfaces;
//using ConexaoCaninaApp.Application.Services;
//using ConexaoCaninaApp.Domain.Models;
//using ConexaoCaninaApp.Infra.Data.Interfaces;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace ConexaoCaninaApp.Domain.Test.Services
//{
//	public class RequisitosCruzamentoServiceTests
//	{
//		private readonly Mock<ICaoRepository> _mockCaoRepository;
//		private readonly IRequisitosCruzamentoService _requisitosCruzamentoService;


//		public RequisitosCruzamentoServiceTests()
//		{
//			_mockCaoRepository = new Mock<ICaoRepository>();	
//			_requisitosCruzamentoService = new RequisitosCruzamentoService(_mockCaoRepository.Object);
//		}

//		[Fact]
//		public async Task AdicionarRequisitos_DeveAdicionarRequisitosAoCao()
//		{
//			var caoId = 1;
//			var requisitos = new RequisitosCruzamentoDto
//			{
//				Temperamento = "Calmo e meigo",
//				Tamanho = "Grande",
//				CaracteristicasGeneticas = "Nenhum problema constatado"
//			};

//			var cao = new Cao
//			{
//				CaoId = caoId,
//				RequisitosCruzamento = new RequisitosCruzamento(), // ou null dependendo do contexto
//			};

//			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

//			await _requisitosCruzamentoService.DefinirRequisitosCruzamento(caoId, requisitos);

//			_mockCaoRepository.Verify(r => r.Atualizar(cao), Times.Once);
//			Assert.Equal(requisitos.Temperamento, cao.RequisitosCruzamento.Temperamento);
//			Assert.Equal(requisitos.Tamanho, cao.RequisitosCruzamento.Tamanho);
//			Assert.Equal(requisitos.CaracteristicasGeneticas, cao.RequisitosCruzamento.CaracteristicasGeneticas);
//		}

//		[Fact]
//		public async Task EditarRequisitos_DeveAtualizarRequisitosDoCao()
//		{
//			var caoId = 1;
//			var requisitosAtualizados = new RequisitosCruzamentoDto
//			{
//				Temperamento = "Calmo e sociável",
//				Tamanho = "Pequeno",
//				CaracteristicasGeneticas = "Nenhum problema constatado"
//			};

//			var cao = new Cao
//			{
//				CaoId = caoId,
//				RequisitosCruzamento = new RequisitosCruzamento
//				{
//					Temperamento = "Agressivo",
//					Tamanho = "Grande",
//					CaracteristicasGeneticas = "Problemas genéticos"
//				}
//			};

//			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);


//			await _requisitosCruzamentoService.EditarRequisitosCruzamento(caoId, requisitosAtualizados);

//			Assert.Equal(requisitosAtualizados.Temperamento, cao.RequisitosCruzamento.Temperamento);
//			Assert.Equal(requisitosAtualizados.Tamanho, cao.RequisitosCruzamento.Tamanho);
//			Assert.Equal(requisitosAtualizados.CaracteristicasGeneticas, cao.RequisitosCruzamento.CaracteristicasGeneticas);

//			_mockCaoRepository.Verify(r => r.Atualizar(cao), Times.Once);
//		}

//		[Fact]
//		public async Task EditarRequisitos_Serve_Para_AtualizarComSucesso_DeveAtualizarRequisitosComSucesso()
//		{

//			var caoId = 1;
//			var requisitosDto = new RequisitosCruzamentoDto
//			{
//				Temperamento = "Agressivo com estranhos mas muito bonito",
//				Tamanho = "Pequeno",
//				CaracteristicasGeneticas = "Problema Genético"
//			};

//			var cao = new Cao
//			{
//				CaoId = caoId,
//				RequisitosCruzamento = new RequisitosCruzamento
//				{
//					Temperamento = "Calmo",
//					Tamanho = "Pequeno",
//					CaracteristicasGeneticas = "Nenhum Problema"
//				}
//			};

//			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);


//			await _requisitosCruzamentoService.EditarRequisitosCruzamento(caoId, requisitosDto);
//			Assert.Equal(requisitosDto.Temperamento, cao.RequisitosCruzamento.Temperamento);
//			Assert.Equal(requisitosDto.Tamanho, cao.RequisitosCruzamento.Tamanho);
//			Assert.Equal(requisitosDto.CaracteristicasGeneticas, cao.RequisitosCruzamento.CaracteristicasGeneticas);
//			_mockCaoRepository.Verify(r => r.Atualizar(cao), Times.Once);
//		}

//		[Fact]
//		public async Task EditarRequisitos_DeveRetornarErroSeCaoNaoExistir()
//		{

//			var caoId = 1;
//			var requisitosDto = new RequisitosCruzamentoDto
//			{
//				Temperamento = "Calmo",
//				Tamanho = "Grande",
//				CaracteristicasGeneticas = "Nenhum Problema, está fazendo exames todos os meses"
//			};

//			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync((Cao)null);

//			await Assert.ThrowsAsync<Exception>(async () =>
//			{
//				await _requisitosCruzamentoService.EditarRequisitosCruzamento(caoId, requisitosDto);
//			});
//		}
//		[Fact]
//		public async Task EditarRequisitos_DeveRetornarErroSeCaoNaoExistir_De_Outra_Perspectiva()
//		{
//			var caoId = 1;
//			var requisitosDto = new RequisitosCruzamentoDto
//			{
//				Temperamento = "Calmo",
//				Tamanho = "Grande",
//				CaracteristicasGeneticas = "Nenhum Problema"
//			};

//			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync((Cao)null);

//			await Assert.ThrowsAsync<Exception>(async () =>
//			{
//				await _requisitosCruzamentoService.EditarRequisitosCruzamento(caoId, requisitosDto);
//			});
//		}

//		[Fact]
//		public async Task EditarRequisitos_DeveAtualizarRequisitosCruzamento()
//		{
//			var caoId = 1;
//			var dto = new RequisitosCruzamentoDto
//			{
//				Temperamento = "Calmo",
//				Tamanho = "Médio",
//				CaracteristicasGeneticas = "Sem problemas genéticos"
//			};

//			var cao = new Cao
//			{
//				CaoId = caoId,
//				RequisitosCruzamento = new RequisitosCruzamento()
//			};

//			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

//			await _requisitosCruzamentoService.EditarRequisitosCruzamento(caoId, dto);

//			Assert.Equal(dto.Temperamento, cao.RequisitosCruzamento.Temperamento);
//			Assert.Equal(dto.Tamanho, cao.RequisitosCruzamento.Tamanho);
//			Assert.Equal(dto.CaracteristicasGeneticas, cao.RequisitosCruzamento.CaracteristicasGeneticas);
//			_mockCaoRepository.Verify(r => r.Atualizar(cao), Times.Once);
//		}
//	}
//}
