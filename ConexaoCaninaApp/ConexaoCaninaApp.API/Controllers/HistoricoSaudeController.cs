using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCaninaApp.API.Controllers
{
	[Authorize(Policy = "RegisteredUserOnly")]
	[ApiController]
	[Route("api/[controller]")]
	public class HistoricoSaudeController : ControllerBase
	{
		private readonly IHistoricoSaudeService _historicoSaudeService;

		public HistoricoSaudeController(IHistoricoSaudeService historicoSaudeService)
		{
			_historicoSaudeService = historicoSaudeService;
		}

		[HttpGet("{caoId}")]
		public async Task<IActionResult> ObterHistoricoSaude(int caoId)
		{
			try
			{
				var historico = await _historicoSaudeService.ObterHistoricoSaudePorCaoId(caoId);

				if (historico == null)
				{
					return NotFound();
				}

				return Ok(historico);
			}
			catch (UnauthorizedAccessException ex)
			{
				return Forbid(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AdicionarHistoricoSaude([FromBody] HistoricoSaudeDto historicoSaudeDto)
		{
			await _historicoSaudeService.AdicionarHistoricoSaude(historicoSaudeDto);

			return Ok();
		}
	}
}
