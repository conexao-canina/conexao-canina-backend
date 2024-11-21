﻿//using ConexaoCaninaApp.Application.Dto;
//using ConexaoCaninaApp.Application.Interfaces;
//using ConexaoCaninaApp.Application.Services;
//using ConexaoCaninaApp.Domain.Models;
//using ConexaoCaninaApp.Infra.Data.Interfaces;
//using Moq;
//using System.Diagnostics;
//using Xunit;

//namespace ConexaoCaninaApp.Domain.Test.Services
//{
//	public class SugestaoServiceTests
//	{
//		private readonly Mock<ISugestaoRepository> _mockSugestaoRepository;
//		private readonly SugestaoService _sugestaoService;
//		private readonly Mock<IUserContextService> _mockUserContextService;
//		private readonly Mock<INotificacaoService> _mockNotificacaoService;



//		public SugestaoServiceTests()
//		{
//			_mockSugestaoRepository = new Mock<ISugestaoRepository>();
//			_mockUserContextService = new Mock<IUserContextService>();
//			_mockNotificacaoService = new Mock<INotificacaoService>();


//			_sugestaoService = new SugestaoService(
//				_mockSugestaoRepository.Object, 
//				_mockUserContextService.Object,
//				_mockNotificacaoService.Object);
//		}

//		[Fact]
//		public async Task ObterSugestoesPorUsuarioAsync_DeveRetornarSugestoesDoUsuario()
//		{
//			var usuarioId = 1;
//			var sugestoes = new List<Sugestao>
//			{
//				new Sugestao { SugestaoId = 1, Descricao = "Sugestão 1", DataEnvio = DateTime.Now, Status = "Em Análise", UsuarioId = usuarioId },
//				new Sugestao { SugestaoId = 2, Descricao = "Sugestão 2", DataEnvio = DateTime.Now, Status = "Aprovada", UsuarioId = usuarioId }
//			};

//			_mockSugestaoRepository.Setup(r => r.ObterSugestoesPorUsuarioAsync(usuarioId))
//				.ReturnsAsync(sugestoes);

//			var resultado = await _sugestaoService.ObterSugestoesPorUsuarioAsync(usuarioId);

//			Assert.NotNull(resultado);
//			Assert.Equal(2, resultado.Count);
//			Assert.Equal("Sugestão 1", resultado[0].Descricao);
//			Assert.Equal("Aprovada", resultado[1].Status);
//			_mockSugestaoRepository.Verify(r => r.ObterSugestoesPorUsuarioAsync(usuarioId), Times.Once);
//		}

//		[Fact]
//		public async Task EnviarSugestao_DeveAdicionarSugestaoComSucesso()
//		{
//			var sugestaoDto = new SugestaoDto { Descricao = "Sugestão de teste" };

//			await _sugestaoService.EnviarSugestaoAsync(sugestaoDto);

//			_mockSugestaoRepository.Verify(r => r.AdicionarAsync(It.IsAny<Sugestao>()), Times.Once);
//		}

//		[Fact]
//		public async Task EnviarFeedback_DeveAtualizarFeedbackCorretamente()
//		{
//			// Arrange
//			var sugestaoId = 1;
//			var feedback = "Obrigado pela sugestão! Estamos avaliando.";
//			var sugestao = new Sugestao
//			{
//				SugestaoId = sugestaoId,
//				Descricao = "Sugestão de exemplo",
//				Status = "Em Análise",
//				Feedback = null,
//				Usuario = new Usuario
//				{
//					Email = "usuario@teste.com"
//				}

//			};

//			_mockUserContextService.Setup(c => c.UsuarioEhAdministrador()).Returns(true);
//			_mockSugestaoRepository.Setup(r => r.ObterPorIdAsync(sugestaoId)).ReturnsAsync(sugestao);

//			// Act
//			await _sugestaoService.EnviarFeedbackAsync(sugestaoId, feedback);

//			// Assert
//			Assert.Equal(feedback, sugestao.Feedback);
//			_mockSugestaoRepository.Verify(r => r.AtualizarAsync(sugestao), Times.Once);
//		}	


//		[Fact]
//		public async Task EnviarFeedback_DeveRetornarErroSeNaoForAdministrador()
//		{
//			var sugestaoId = 1;
//			var feedback = "Obrigado pela sugestão! Estamos avaliando.";

//			_mockUserContextService.Setup(c => c.UsuarioEhAdministrador()).Returns(false);


//			var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
//				async () => await _sugestaoService.EnviarFeedbackAsync(sugestaoId, feedback));

//			Assert.Equal("Somente administradores podem enviar feedback.", exception.Message);
//		}

//		[Fact]
//		public async Task ObterSugestao_DeveRetornarFeedback()
//		{
//			// Arrange
//			var sugestaoId = 1;
//			var feedbackEsperado = "Esta é uma sugestão de teste";
//			var sugestao = new Sugestao
//			{
//				SugestaoId = sugestaoId,
//				Descricao = "Sugestão de exemplo",
//				Status = "Em Análise",
//				Feedback = feedbackEsperado
//			};

//			_mockSugestaoRepository.Setup(r => r.ObterPorIdAsync(sugestaoId))
//				.ReturnsAsync(sugestao);
//			_mockSugestaoRepository.Setup(r => r.ObterSugestoesPorUsuarioAsync(It.IsAny<int>()))
//				.ReturnsAsync(new List<Sugestao>
//				{
//					new Sugestao{ 
//						SugestaoId = sugestaoId,  Descricao = "Sugestão de exemplo", Status = "Em Análise",  Feedback = feedbackEsperado }
					
//				});

//			var resultado = await _sugestaoService.ObterSugestoesPorUsuarioAsync(sugestaoId);

			
//			var sugestaoRetornada = resultado.FirstOrDefault(s => s.SugestaoId == sugestaoId);
//			Assert.NotNull(sugestaoRetornada);
//			Assert.Equal(feedbackEsperado, sugestaoRetornada.Feedback);
//		}
//		[Fact]
//		public async Task EnviarSugestao_DeveSalvarSugestaoRapidamente()
//		{
//			// Arrange
//			var stopwatch = new Stopwatch();
//			var sugestaoDto = new SugestaoDto
//			{
//				Descricao = "Sugestão de teste",
//				UsuarioId = 1
//			};

//			// Act
//			stopwatch.Start();
//			await _sugestaoService.EnviarSugestaoAsync(sugestaoDto);
//			stopwatch.Stop();

//			// Assert
//			Assert.True(stopwatch.ElapsedMilliseconds < 500, "O tempo de salvamento é muito alto.");
//		}

//		[Fact]	
//		public async Task EnviarSugestao_DeveSalvarSugestaoCorretamente()
//		{
//			var sugestaoDto = new SugestaoDto
//			{
//				Descricao = "Esta é uma nova sugestão.",
//				UsuarioId = 1
//			};

//			await _sugestaoService.EnviarSugestaoAsync(sugestaoDto);
//			_mockSugestaoRepository.Verify(r => r.AdicionarAsync(It.IsAny<Sugestao>()), Times.Once);

//		}

//		[Fact]
//		public async Task ObterSugestoesPorUsuario_DeveRetornarSugestoesCorretamente()
//		{
//			var usuarioId = 1;
//			var sugestoes = new List<Sugestao>
//			{
//				new Sugestao {  SugestaoId = 1, Descricao = "Sugestão 1", UsuarioId = usuarioId, Status = "Em Análise" },
//				new Sugestao {  SugestaoId = 2, Descricao = "Sugestão 2", UsuarioId = usuarioId, Status = "Aprovada" }
//			};

//			_mockSugestaoRepository.Setup(r => r.ObterSugestoesPorUsuarioAsync(usuarioId)).ReturnsAsync(sugestoes);

//			var resultado = await _sugestaoService.ObterSugestoesPorUsuarioAsync(usuarioId);

//			Assert.NotNull(resultado);
//			Assert.Equal(2, resultado.Count);
//			Assert.Equal("Sugestão 1", resultado.First().Descricao);
//		}

//		[Fact]
//		public async Task EnviarFeedback_DeveDispararNotificacao()
//		{
//			var sugestaoId = 1;
//			var feedback = "Obrigado pela sugestão!";
//			var sugestao = new Sugestao
//			{ SugestaoId = sugestaoId, Usuario = new Usuario { Email = "user@example.com" } };

//			_mockSugestaoRepository.Setup(r => r.ObterPorIdAsync(sugestaoId)).ReturnsAsync(sugestao);
//			_mockUserContextService.Setup(c => c.UsuarioEhAdministrador()).Returns(true);

//			await _sugestaoService.EnviarFeedbackAsync(sugestaoId, feedback);

//			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoParaSugestao(
//				sugestao.Usuario.Email,
//				"Feedback Recebido",
//				 $"Você recebeu feedback em sua sugestão: {feedback}"), Times.Once);

//		}

//	}
//}
