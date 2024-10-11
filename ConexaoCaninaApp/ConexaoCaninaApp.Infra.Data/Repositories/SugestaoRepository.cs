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
	public class SugestaoRepository : ISugestaoRepository
	{
		private readonly ApplicationDbContext _context;

		public SugestaoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Sugestao> ObterPorIdAsync(int sugestaoId)
		{
			return await _context.Sugestoes.FirstOrDefaultAsync(s => s.SugestaoId == sugestaoId);
		}
		public async Task<List<Sugestao>> ObterSugestoesPorUsuarioAsync(int usuarioId)
		{
			return await _context.Sugestoes
				.Where(s => s.UsuarioId == usuarioId)
				.ToListAsync();
		}
		public async Task AdicionarAsync(Sugestao sugestao)
		{
			_context.Sugestoes.Add(sugestao);	
			await _context.SaveChangesAsync();
		}
		public async Task AtualizarAsync(Sugestao sugestao)
		{
			_context.Sugestoes.Update(sugestao);
			await _context.SaveChangesAsync();
		}
	}
}
