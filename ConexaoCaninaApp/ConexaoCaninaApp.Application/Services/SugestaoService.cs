using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class SugestaoService : ISugestaoService
	{
		private readonly ISugestaoRepository _sugestaoRepository;
		private readonly IUserContextService _userContextService;
		private readonly INotificacaoService _notificacaoService;

		public SugestaoService(ISugestaoRepository sugestaoRepository, IUserContextService userContextService,
			INotificacaoService notificacaoService)
		{
			_sugestaoRepository = sugestaoRepository;
			_userContextService = userContextService;
			_notificacaoService = notificacaoService;
		}

		public async Task<List<SugestaoDto>> ObterSugestoesPorUsuarioAsync(int usuarioId)
		{
			var sugestoes = await _sugestaoRepository.ObterSugestoesPorUsuarioAsync(usuarioId);
			var sugestoesDto = sugestoes.Select(s => new SugestaoDto
			{
				SugestaoId = s.SugestaoId,
				Descricao = s.Descricao,
				DataEnvio = s.DataEnvio,
				Feedback = s.Feedback,
				Status = s.Status
			}).ToList();

			return sugestoesDto;
		}
		public async Task EnviarSugestaoAsync(SugestaoDto sugestaoDto)
		{
			var sugestao = new Sugestao
			{
				Descricao = sugestaoDto.Descricao,
				DataEnvio = DateTime.Now,
				UsuarioId = sugestaoDto.UsuarioId,
				Status = "Em Análise"
			};

			await _sugestaoRepository.AdicionarAsync(sugestao);
		}

		public async Task EnviarFeedbackAsync(int sugestaoId, string feedback)
		{
			if (!_userContextService.UsuarioEhAdministrador())
				throw new UnauthorizedAccessException("Somente administradores podem enviar feedback.");

			var sugestao = await _sugestaoRepository.ObterPorIdAsync(sugestaoId);

			if (sugestao == null) throw new Exception("Sugestão não encontrada.");
			sugestao.Feedback = feedback;

			await _sugestaoRepository.AtualizarAsync(sugestao);
			await _notificacaoService.EnviarNotificacaoParaSugestao(
				sugestao.Usuario.Email,
				"Feedback Recebido", 
				$"Você recebeu feedback em sua sugestão: {feedback}");

		}

	}
}
