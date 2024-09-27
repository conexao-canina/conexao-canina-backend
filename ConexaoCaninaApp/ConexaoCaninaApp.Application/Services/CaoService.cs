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
	public class CaoService : ICaoService
	{
		private readonly ICaoRepository _caoRepository;
		private readonly INotificacaoService _notificacaoService;

		public CaoService(ICaoRepository caoRepository, INotificacaoService notificacaoService)
		{
			_caoRepository = caoRepository;
			_notificacaoService = notificacaoService;
		}

		public async Task<Cao> AdicionarCao(CaoDto caoDto)
		{
			var cao = new Cao
			{
				Nome = caoDto.Nome,
				Raca = caoDto.Raca,
				Idade = caoDto.Idade,
				Descricao = caoDto.Descricao,
				Genero = caoDto.Genero,
				Tamanho = caoDto.Tamanho,
				CaracteristicasUnicas = caoDto.CaracteristicasUnicas,
				ProprietarioId = caoDto.ProprietarioId,
				Status = StatusCao.Pendente,
			};

			await _caoRepository.Adicionar(cao);
			await _notificacaoService.EnviarNotificacaoParaAdministrador(cao);

			return cao;
		}

		public async Task AprovarCao(int caoId)
		{
			var cao = await _caoRepository.ObterPorId(caoId);
			cao.Status = StatusCao.Aprovado;
			await _caoRepository.Atualizar(cao);

			await _notificacaoService.EnviarNotificacaoParaUsuario(cao);
		}

		public async Task<Cao> ObterPorId(int id)
		{
			return await _caoRepository.ObterPorId(id);
		}
	}
}
