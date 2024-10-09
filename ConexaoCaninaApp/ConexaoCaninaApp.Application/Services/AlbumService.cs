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
	public class AlbumService : IAlbumService
	{
		private readonly IAlbumRepository _albumRepository;

		public AlbumService(IAlbumRepository albumRepository)
		{
			_albumRepository = albumRepository;
		}

		public async Task CriarAlbum(AlbumDto albumDto)
		{
			var album = new Album
			{
				Nome = albumDto.Nome,
				Descricao = albumDto.Descricao
			};

			await _albumRepository.Adicionar(album);
		}
	}
}
