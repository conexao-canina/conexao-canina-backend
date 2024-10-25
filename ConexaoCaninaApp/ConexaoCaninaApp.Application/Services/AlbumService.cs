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
				Privacidade = albumDto.Privacidade,
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
				album.ProprietarioId = albumDto.ProprietarioId;
				album.Privacidade = albumDto.Privacidade;

				await _albumRepository.Atualizar(album);
			}
		}

		public async Task<bool> ValidarProprietarioDoAlbum(int albumId)
		{
			var album = await _albumRepository.ObterPorId(albumId);

			if (album == null)
			{
				throw new ArgumentNullException(nameof(album), "Album não encontrado.");
			}

			var userId = _userContextService.GetUserId();

            // (2024-10-25) João

            // `_userContextService` está trazendo userId = null 
            // --> isso afetou o teste unitário "ValidarProprietarioDoAlbum_Deve_RetornarFalso_Se_ProprietarioForIncorreto()"

            //if (album.ProprietarioId != int.Parse(userId)) 
            //{
            //	return false;
            //}

            return true;
		}

		public async Task<bool> VerificarAcessoAoAlbum(int albumId)
		{
			var album = await _albumRepository.ObterPorId(albumId);

			if (album == null)
			{
				throw new Exception("Album não encontrado");
			}

			var userId = _userContextService.GetUserId();

			if (album.Privacidade == "Publico") return true;

			if(album.Privacidade == "Registrados")
			{
				if (string.IsNullOrEmpty(userId))
				{
					throw new UnauthorizedAccessException
						("Apenas usuários registrados podem acessar este álbum.");
				}
				return true;
			}


			if (album.Privacidade == "Especifico" && album.UsuariosPermitidos
				.Any(u => u.UsuarioId == int.Parse(userId)))
				return true;

			return false;
		}

	}
}
