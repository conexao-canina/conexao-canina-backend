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

		public SugestaoService(ISugestaoRepository sugestaoRepository)
		{
			_sugestaoRepository = sugestaoRepository;
		}

		public async Task<List<SugestaoDto>> ObterSugestoesPorUsuarioAsync(int usuarioId)
		{
			var sugestoes = await _sugestaoRepository.ObterSugestoesPorUsuarioAsync(usuarioId);
			var sugestoesDto = sugestoes.Select(s => new SugestaoDto
			{
				SugestaoId = s.SugestaoId,
				Descricao = s.Descricao,
				DataEnvio = s.DataEnvio,
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
	}
}
