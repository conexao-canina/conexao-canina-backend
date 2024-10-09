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
	public class SolicitacaoCruzamentoService :ISolicitacaoCruzamentoService
	{
		private readonly ISolicitacaoCruzamentoRepository _solicitacaoRepository;
		private readonly INotificacaoService _notificacaoService;

		public SolicitacaoCruzamentoService(ISolicitacaoCruzamentoRepository solicitacaoRepository, INotificacaoService notificacaoService)
		{
			_solicitacaoRepository = solicitacaoRepository;
			_notificacaoService = notificacaoService;
		}

		public async Task EnviarSolicitacaoAsync(SolicitacaoCruzamentoDto solicitacaoDto)
		{
			var solicitacao = new SolicitacaoCruzamento
			{
				UsuarioId = solicitacaoDto.UsuarioId,
				CaoId = solicitacaoDto.CaoId,
				Mensagem = solicitacaoDto.Mensagem,
				DataSolicitacao = DateTime.Now
			};

			await _solicitacaoRepository.Adicionar(solicitacao);

			var cao = solicitacaoDto.Cao; 
			var emailUsuario = cao.Proprietario.Email;
			var nomeDoCao = cao.Nome;

			await _notificacaoService.EnviarNotificacaoSolicitacaoCruzamento(emailUsuario, nomeDoCao, solicitacaoDto.Mensagem);

		}
	}
}
