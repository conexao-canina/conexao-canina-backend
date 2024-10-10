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
	public class SugestaoRepository : ISugestaoRepository
	{
		private readonly ApplicationDbContext _context;

		public SugestaoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task AdicionarAsync(Sugestao sugestao)
		{
			_context.Sugestoes.Add(sugestao);	
			await _context.SaveChangesAsync();
		}
	}
}
