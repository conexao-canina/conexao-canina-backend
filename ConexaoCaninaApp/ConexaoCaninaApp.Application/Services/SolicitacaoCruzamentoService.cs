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

		public SolicitacaoCruzamentoService(ISolicitacaoCruzamentoRepository solicitacaoRepository)
		{
			_solicitacaoRepository = solicitacaoRepository;
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
		} 
	}
}
