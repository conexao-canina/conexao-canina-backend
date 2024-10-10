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
