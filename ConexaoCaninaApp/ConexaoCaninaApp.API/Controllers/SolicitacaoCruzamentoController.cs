using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SolicitacaoCruzamentoController : ControllerBase
	{
		private readonly ISolicitacaoCruzamentoService _solicitacaoService;


		public SolicitacaoCruzamentoController(ISolicitacaoCruzamentoService solicitacaoService)
		{
			_solicitacaoService = solicitacaoService;
		}

		[HttpPost]
		public async Task<IActionResult> EnviarSolicitacao([FromBody] SolicitacaoCruzamentoDto solicitacaoDto)
		{
			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);

			return Ok("Solicitação de cruzamento enviada com sucesso.");
		}
	}
}
