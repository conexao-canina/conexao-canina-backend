using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Context;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Repositories
{
	public class SolicitacaoCruzamentoRepository : ISolicitacaoCruzamentoRepository
	{
		private readonly ApplicationDbContext _context;

		public SolicitacaoCruzamentoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Adicionar(SolicitacaoCruzamento solicitacao)
		{
			_context.SolicitacoesCruzamento.Add(solicitacao);
			await _context.SaveChangesAsync();
		}

		public async Task<SolicitacaoCruzamento> ObterPorId(int id)
		{
			return await _context.SolicitacoesCruzamento.FirstOrDefaultAsync(s => s.SolicitacaoId == id);
		}

		public async Task Atualizar(SolicitacaoCruzamento solicitacao)
		{
			_context.SolicitacoesCruzamento.Update(solicitacao);	

			await _context.SaveChangesAsync();
		}
	}
}
