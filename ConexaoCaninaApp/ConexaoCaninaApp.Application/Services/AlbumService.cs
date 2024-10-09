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
		private readonly IUserContextService _userContextService;

		public AlbumService(IAlbumRepository albumRepository, IUserContextService userContextService)
		{
			_albumRepository = albumRepository;
			_userContextService = userContextService;
		}

		public async Task CriarAlbum(AlbumDto albumDto)
		{
			var album = new Album
			{
				Nome = albumDto.Nome,
				Descricao = albumDto.Descricao,
				ProprietarioId = albumDto.ProprietarioId,
			};

			await _albumRepository.Adicionar(album);
		}

		public async Task EditarAlbumAsync(int albumId, AlbumDto albumDto)
		{
			if (! await ValidarProprietarioDoAlbum(albumId))
			{
				throw new UnauthorizedAccessException
					("Você não tem permissão para editar este álbum.");
			}
			var album = await _albumRepository.ObterPorId(albumId);

			if (album != null)
			{
				album.Nome = albumDto.Nome;
				album.Descricao = albumDto.Descricao;

				await _albumRepository.Atualizar(album);
			}
		}

		public async Task<bool> ValidarProprietarioDoAlbum(int albumId)
		{
			var album = await _albumRepository.ObterPorId(albumId);

			if (album == null)
			{
				throw new Exception("Album não encontrado.");
			}

			var userId = _userContextService.GetUserId();

			if (album.ProprietarioId != int.Parse(userId))
			{
				return false;
			}

			return true;
		}


	}
}
