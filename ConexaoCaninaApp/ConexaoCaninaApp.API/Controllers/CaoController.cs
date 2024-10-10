using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CaoController : ControllerBase
	{
		private readonly ICaoService _caoService;
		private readonly IUserContextService _userContextService;

		public CaoController(ICaoService caoService, IUserContextService userContextService)
		{
			_caoService = caoService;
			_userContextService = userContextService;
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> ObterCao(int id)
		{
			var cao = await _caoService.ObterPorId(id);
			if (cao == null)
			{
				return NotFound();
			}
			return Ok(cao);
			
		}

		[HttpGet("{id}/detalhes")]
		public async Task<IActionResult> ObterDetalhesCao(int id)
		{
			var cao = await _caoService.ObterPorId(id);

			if (cao == null)
			{
				return NotFound();
			}

			var detalhes = new
			{
				Nome = cao.Nome,
				Raca = cao.Raca,
				Idade = cao.Idade,
				Sexo = cao.Genero,
				Caracteristicas = cao.CaracteristicasUnicas,
				Descricao = cao.Descricao
			};

			return Ok(detalhes);
		}
		

		[HttpGet("verificar-permissao/{caoId}/{usuarioId}")]
		public async Task<IActionResult> VerificarPermissao(int caoId, int usuarioId)
		{
			var temPermissao = await _caoService.VerificarPerimissaoEdicao(caoId, usuarioId);
			if (!temPermissao)
			{
				return Forbid(); // caso o usuario nao tiver permissao
			}

			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> AdicionarCao([FromBody] AdicionarCaoComModerarPerfilDto dto)
		{
			var cao = await _caoService.AdicionarCao(dto.CaoDto, dto.ModerarPerfilDto);
			return CreatedAtAction(nameof(ObterCao), new { id = cao.CaoId }, cao);
		}

		[HttpPut("{id}/aprovar")]
		public async Task<IActionResult> AprovarCao(int id)
		{
			await _caoService.AprovarCao(id);
			return NoContent();
		}

		[HttpPut("editar")]
		public async Task<IActionResult> EditarCao([FromBody] EditarCaoComModerarPerfilDto dto)
		{
			await _caoService.AtualizarCao(dto.EditarCaoDto, dto.ModerarPerfilDto);
			return Ok();
		}

		[HttpPut("{id}/atualizar-informacoes")]
		public async Task<IActionResult> AtualizarInformacoesBasicas(int id, [FromBody] AtualizarInformacoesBasicasDto dto)
		{
			try
			{
				await _caoService.AtualizarInformacoesBasicas(id, dto);
				return NoContent(); // essa é a resposta padrão para as atualizações
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("publicar/{id}")]
		public async Task<IActionResult> PublicarCao(int id)
		{
			await _caoService.PublicarCao(id);
			return Ok();
		}


		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> ExcluirCao(int id)
		{
			await _caoService.ExcluirCao(id);
			return Ok(new { message =
				"Perfil excluído com sucesso. A exclusão é permanente"
			});
		}

		[HttpPut("{id}/moderar")]
		public async Task<IActionResult> ModerarPerfilCao(int id, [FromBody] ModerarPerfilDto moderarPerfilDto)
		{
			await _caoService.ModerarPerfil(id, moderarPerfilDto);
			return NoContent();
		}



		[HttpPost("{caoId}/like")]
		public async Task<IActionResult> DarLike(int caoId)
		{
			var usuarioIdString = _userContextService.GetUserId();
			var usuarioId = int.Parse(usuarioIdString);
			await _caoService.DarLike(caoId, usuarioId);
			return Ok("Like adicionado com sucesso.");
		}


		[HttpDelete("{caoId}/like")]
		public async Task<IActionResult> RemoverLike(int caoId)
		{
			var usuarioIdString = _userContextService.GetUserId();
			var usuarioId = int.Parse(usuarioIdString);
			await _caoService.RemoverLike(caoId, usuarioId);
			return Ok("Like removido");
		}


	}
}
