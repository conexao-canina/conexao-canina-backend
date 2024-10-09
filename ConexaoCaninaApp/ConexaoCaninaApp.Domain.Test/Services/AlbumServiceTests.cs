using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConexaoCaninaApp.Domain.Test.Services
{
	public class AlbumServiceTests
	{
		private readonly AlbumService _albumService;
		private readonly Mock<IAlbumRepository> _mockAlbumRepository;
		private readonly Mock<IUserContextService> _mockUserContextService;

		public AlbumServiceTests()
		{
			_mockAlbumRepository = new Mock<IAlbumRepository>();
			_mockUserContextService = new Mock<IUserContextService>();


			_albumService = new AlbumService( _mockAlbumRepository.Object, _mockUserContextService.Object  );
		}

		[Fact]
		public async Task CriarAlbum_Deve_Chamar_Adicionar_Album()
		{
			// ARRANGE

			var albumDto = new AlbumDto 
			{
				Nome = "Novo Album", 
				Descricao = "desc" 
			};

			// ACT

			await _albumService.CriarAlbum( albumDto );

			// ASSERT

			_mockAlbumRepository.Verify(repo => repo.Adicionar(It.IsAny<Album>()), Times.Once);
		}

		[Fact]
		public async Task ValidarProprietarioDoAlbum_Deve_RetornarVerdadeiro_Se_ProprietarioForCorreto()
		{
			var albumId = 1;
			var userId = "123";
			var album = new Album
			{
				AlbumId = albumId,
				ProprietarioId = 123
			};

			_mockAlbumRepository.Setup(r => r.ObterPorId(albumId)).ReturnsAsync(album);
			_mockUserContextService.Setup(s => s.GetUserId()).Returns(userId);

			var resultado = await _albumService.ValidarProprietarioDoAlbum(albumId);


			Assert.True(resultado);

		}

		[Fact]
		public async Task ValidarProprietarioDoAlbum_Deve_RetornarFalso_Se_ProprietarioForIncorreto()

		{
			var albumId = 1;
			var userId = "456";
			var album = new Album
			{
				AlbumId = albumId,
				ProprietarioId = 123
			}; 

			_mockAlbumRepository.Setup(r => r.ObterPorId(albumId)).ReturnsAsync(album);
			_mockUserContextService.Setup(s => s.GetUserId()).Returns(userId);


			var resultado = await _albumService.ValidarProprietarioDoAlbum(albumId);

			Assert.False(resultado);

		}

		[Fact]
		public async Task EditarAlbum_Deve_AtualizarNomeEDescricaoDoAlbum()
		{
			var albumId = 1;
			var albumDto = new AlbumDto
			{
				Nome = "Novo nome",
				Descricao = "Nova descricao",
				ProprietarioId = 1
			};

			var album = new Album
			{
				AlbumId = albumId,
				Nome = "Nome mais antigo",
				Descricao = "Descricao mais antiga",
				ProprietarioId = 1
			};

			_mockAlbumRepository.Setup(r => r.ObterPorId(albumId)).ReturnsAsync(album);
			_mockUserContextService.Setup(u => u.GetUserId()).Returns("1"); 


			await _albumService.EditarAlbumAsync(albumId, albumDto);


			_mockAlbumRepository.Verify(r => r.Atualizar(It.Is<Album>(a => a.Nome == albumDto.Nome && a.Descricao ==
			albumDto.Descricao)),  Times.Once);

		}

		[Fact]
		public async Task VerificarAcessoAoAlbum_Deve_Permitir_Acesso_Se_Publico()
		{
			var albumId = 1;
			var album = new Album
			{
				AlbumId = albumId,
				Privacidade = "Publico"
			};
			_mockAlbumRepository.Setup(repo => repo.ObterPorId(albumId)).ReturnsAsync(album);

			var result = await _albumService.VerificarAcessoAoAlbum(albumId);


			Assert.True(result);

		}

		[Fact]
		public async Task VerificarAcessoAoAlbum_Deve_Bloquear_Acesso_Se_Usuario_Nao_Registrado()
		{
			var albumId = 1;
			var album = new Album
			{
				AlbumId = albumId,
				Privacidade = "Registrados"
			};

			_mockAlbumRepository.Setup(repo => repo.ObterPorId(albumId)).ReturnsAsync(album);
			_mockUserContextService.Setup(service => service.GetUserId()).Returns((string)null);

			await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
			{
				await _albumService.VerificarAcessoAoAlbum(albumId);
			});
		}
	}	
}
