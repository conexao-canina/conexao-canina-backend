using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SugestaoController : ControllerBase
	{
		private readonly ISugestaoService _sugestaoService;

		public SugestaoController(ISugestaoService sugestaoService)
		{
			_sugestaoService = sugestaoService;
		}

		[HttpGet("{usuarioId}/sugestoes")]

		public async Task<IActionResult> ConsultarSugestoesPorUsuario(int usuarioId)
		{
			try
			{
				var sugestoes = await _sugestaoService.ObterSugestoesPorUsuarioAsync(usuarioId);
				return Ok(sugestoes);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost]
		public async Task<IActionResult> EnviarSugestao([FromBody] SugestaoDto sugestaoDto)
		{
			try
			{
				await _sugestaoService.EnviarSugestaoAsync(sugestaoDto);
				return Ok("Sugestão enviada com sucesso.");
			}
			catch (Exception ex) { return BadRequest(ex); }
		}
	}
}
