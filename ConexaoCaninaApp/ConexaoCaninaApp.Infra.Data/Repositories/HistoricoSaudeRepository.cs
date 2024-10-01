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
	public class HistoricoSaudeRepository : IHistoricoSaudeRepository
	{
		private readonly ApplicationDbContext _context;

		public HistoricoSaudeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<HistoricoSaude>> ObterHistoricoPorCaoId(int caoId)
		{
			return await _context.HistoricosDeSaude
				.Where(h => h.CaoId == caoId)
				.ToListAsync();
		}

		public async Task AdicionarHistorico(HistoricoSaude historicoSaude)
		{
			_context.HistoricosDeSaude.Add(historicoSaude);
			await _context.SaveChangesAsync();
		}
	}
}
