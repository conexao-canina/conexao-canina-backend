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
		private readonly IRequisitosCruzamentoService _requisitosCruzamentoService;


		public SolicitacaoCruzamentoController(ISolicitacaoCruzamentoService solicitacaoService, IRequisitosCruzamentoService requisitosCruzamentoService)
		{
			_solicitacaoService = solicitacaoService;
			_requisitosCruzamentoService = requisitosCruzamentoService;
		}

		[HttpPost]
		public async Task<IActionResult> EnviarSolicitacao([FromBody] SolicitacaoCruzamentoDto solicitacaoDto)
		{
			await _solicitacaoService.EnviarSolicitacaoAsync(solicitacaoDto);

			return Ok("Solicitação de cruzamento enviada com sucesso.");
		}

		[HttpPost("{caoId}/requisitos")]
		public async Task<IActionResult> DefinirRequisitos(int caoId, [FromBody] RequisitosCruzamentoDto dto)
		{
			await _requisitosCruzamentoService.DefinirRequisitosCruzamento(caoId, dto);

			return Ok("Requisitos definidos com sucesso");
		}

		[HttpPut("{caoId}/requisitos")]
		public async Task<IActionResult> EditarRequisitos(int caoId, [FromBody] RequisitosCruzamentoDto dto)
		{
			try
			{
				await _requisitosCruzamentoService.EditarRequisitosCruzamento(caoId, dto);
				return Ok("Requisitos atualizados com sucesso.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}/aceitar")]
		public async Task<IActionResult> AceitarSolicitacao(int id)
		{
			try
			{
				await _solicitacaoService.AceitarSolicitacaoAsync(id);
				return Ok("Solicitação de cruzamento aceita.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}/rejeitar")]
		public async Task<IActionResult> RejeitarSolicitacao(int id)
		{
			try
			{
				await _solicitacaoService.RejeitarSolicitacaoAsync(id);
				return Ok("Solicitação de cruzamento rejeitada.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
