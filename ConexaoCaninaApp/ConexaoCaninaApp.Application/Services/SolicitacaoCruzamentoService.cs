﻿using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConexaoCaninaApp.Domain.Models.SolicitacaoCruzamento;

namespace ConexaoCaninaApp.Application.Services
{
	public class SolicitacaoCruzamentoService :ISolicitacaoCruzamentoService
	{
		private readonly ISolicitacaoCruzamentoRepository _solicitacaoRepository;
		private readonly INotificacaoService _notificacaoService;
		private readonly ICaoRepository _caoRepository;



		public SolicitacaoCruzamentoService(ISolicitacaoCruzamentoRepository solicitacaoRepository, 
			INotificacaoService notificacaoService, ICaoRepository caoRepository)
		{
			_solicitacaoRepository = solicitacaoRepository;
			_notificacaoService = notificacaoService;
			_caoRepository = caoRepository;
		}

		public async Task EnviarSolicitacaoAsync(SolicitacaoCruzamentoDto solicitacaoDto)
		{
			var cao = await _caoRepository.ObterPorId(solicitacaoDto.CaoId);
			if (cao == null)
			{
				throw new NullReferenceException("O objeto Cao está nulo.");

			}
			var solicitacao = new SolicitacaoCruzamento
			{
				UsuarioId = solicitacaoDto.UsuarioId,
				CaoId = solicitacaoDto.CaoId,
				Mensagem = solicitacaoDto.Mensagem,
				DataSolicitacao = DateTime.Now
			};

			await _solicitacaoRepository.Adicionar(solicitacao);

			var emailUsuario = cao.Proprietario.Email;
			var nomeDoCao = cao.Nome;

			await _notificacaoService.EnviarNotificacaoSolicitacaoCruzamento(emailUsuario, nomeDoCao, solicitacaoDto.Mensagem);

		}

		public async Task<bool> ValidarSolicitacaoAsync(SolicitacaoCruzamentoDto solicitacaoDto)
		{
			var cao = await _caoRepository.ObterPorId(solicitacaoDto.CaoId);

			if (cao == null || cao.RequisitosCruzamento == null) 
			{ throw new Exception("Cão ou requisitos nao encontrados"); }

			var requisitos = cao.RequisitosCruzamento;

			bool atendeRequisitos =
				solicitacaoDto.Cao.RequisitosCruzamento.Temperamento == requisitos.Temperamento &&
				solicitacaoDto.Cao.RequisitosCruzamento.Tamanho == requisitos.Tamanho &&
				solicitacaoDto.Cao.RequisitosCruzamento.CaracteristicasGeneticas == requisitos.CaracteristicasGeneticas;

			return atendeRequisitos;

		}

		public async Task AceitarSolicitacaoAsync(int solicitacaoId)
		{
			var solicitacao = await _solicitacaoRepository.ObterPorId(solicitacaoId);

			if (solicitacao == null)
			{
				throw new Exception("Solicitação não encontrada");
			}

			solicitacao.Status = StatusSolicitacao.Aceita;

			await _solicitacaoRepository.Atualizar(solicitacao);
		}

		public async Task RejeitarSolicitacaoAsync(int solicitacaoId)
		{
			var solicitacao = await _solicitacaoRepository.ObterPorId(solicitacaoId);

			if (solicitacao == null)
			{
				throw new Exception("Solicitação não encontrada");
			}

			solicitacao.Status = StatusSolicitacao.Rejeitada;

			var requisitosNaoAtendidos = "Exemplo: Temperamento não compatível, Tamanho não adequado"; 
			await _notificacaoService.EnviarNotificacaoSolicitacaoRejeitada
				(solicitacao.Usuario.Email, solicitacao.Cao.Nome, requisitosNaoAtendidos);

			await _solicitacaoRepository.Atualizar(solicitacao);


		}
	}
}
