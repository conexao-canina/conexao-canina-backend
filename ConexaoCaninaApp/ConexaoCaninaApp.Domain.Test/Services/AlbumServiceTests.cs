using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConexaoCaninaApp.Domain.Test.Services
{
	public class AlbumServiceTests
	{
		private readonly AlbumService _albumService;
		private readonly Mock<IAlbumRepository> _mockAlbumRepository;

		public AlbumServiceTests()
		{
			_mockAlbumRepository = new Mock<IAlbumRepository>();
			_albumService = new AlbumService( _mockAlbumRepository.Object );
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
	}	
}
