﻿using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FotoController : ControllerBase
	{
		private readonly IFotoService _fotoService;

		public FotoController(IFotoService fotoService)
		{
			_fotoService = fotoService;
		}

		[HttpPost("{caoId}/upload")]
		public async Task<IActionResult> UploadFotos([FromRoute] int caoId, [FromForm] List<IFormFile> arquivos)
		{
			var fotos = await _fotoService.UploadFotosAsync(arquivos, caoId);
			return Ok(fotos);
		}

		[HttpPut("reordenar")]
		public async Task<IActionResult> ReordenarFotos([FromBody] IEnumerable<FotoDto> fotos)
		{
			await _fotoService.ReordenarFotosAsync(fotos);
			return NoContent();
		}

		[HttpDelete("{fotoId}")]
		public async Task<IActionResult> ExcluirFoto(int fotoId)
		{
			await _fotoService.ExcluirFotoAsync(fotoId);
			return NoContent();
		}
	}
}