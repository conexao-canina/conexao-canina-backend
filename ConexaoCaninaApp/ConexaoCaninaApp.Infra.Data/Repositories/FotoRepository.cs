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
	public class FotoRepository : IFotoRepository
	{
		private readonly ApplicationDbContext _context;

		public FotoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Adicionar(Foto foto)
		{
			_context.Fotos.Add(foto);
			await _context.SaveChangesAsync();
		}

		public async Task<Foto> ObterPorId(int id)
		{
			return await _context.Fotos.FindAsync(id);
		}
		 
		public async Task<IEnumerable<Foto>> ObterFotosPorCaoId(int caoId)
		{
			return await _context.Fotos
				.Where(f => f.CaoId == caoId)	
				.ToListAsync();
		}

		public async Task Remover(Foto foto)
		{
			_context.Fotos.Remove(foto);	
			await _context.SaveChangesAsync();
		}

		public async Task Atualizar(Foto foto)
		{
			_context.Fotos.Update(foto);
		}

		public async Task<int> ObterProximaOrdemAsync(int caoId)
		{
			var ultimaFoto = await _context.Fotos
				.Where(f => f.CaoId == caoId)
				.OrderByDescending(f => f.Ordem)
				.FirstOrDefaultAsync();

			return ultimaFoto?.Ordem + 1 ?? 1; ;
		}

	}
}
