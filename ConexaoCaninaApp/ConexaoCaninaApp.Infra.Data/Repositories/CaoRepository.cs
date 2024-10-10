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
	public class CaoRepository : ICaoRepository
	{
		private readonly ApplicationDbContext _context;
		public CaoRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Adicionar(Cao cao)
		{
			_context.Caes.Add(cao);
			await _context.SaveChangesAsync();
		}

		public async Task<Cao> ObterPorId(int id)
		{
			return await _context.Caes
				.Include(c => c.Fotos)
				.Include(c => c.Proprietario)
				.Include(c => c.Likes)
				.FirstOrDefaultAsync(c => c.CaoId == id);
		}

		public async Task Atualizar(Cao cao)
		{
			_context.Caes.Update(cao);
			await _context.SaveChangesAsync();
		}

		public async Task Remover(Cao cao)
		{
			_context.Caes.Remove(cao);
			await _context.SaveChangesAsync();
		}
	}
}
