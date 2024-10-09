using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AlbumController : ControllerBase
	{
		private readonly IAlbumService _albumService;

		public AlbumController(IAlbumService albumService)
		{
			_albumService = albumService;
		}


		[HttpPost]
		public async Task<IActionResult> CriarAlbum([FromBody] AlbumDto albumDto)
		{
			await _albumService.CriarAlbum(albumDto);

			return Ok("Álbum criado com sucesso");


		}
	}
}
