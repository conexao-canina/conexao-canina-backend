using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Context;
using ConexaoCaninaApp.Infra.Data.Interfaces;
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
	}
}
