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
	public class AlbumRepository : IAlbumRepository
	{
		private readonly ApplicationDbContext _context;

		public AlbumRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Adicionar(Album album)
		{
			_context.Albuns.Add(album);

			await _context.SaveChangesAsync();
		}

		public async Task Atualizar(Album album)
		{
			_context.Albuns.Update(album);

			await _context.SaveChangesAsync();
		}

		public async Task<Album> ObterPorId(int albumId)
		{
			return await _context.Albuns.FindAsync(albumId);
		}
	}
}
