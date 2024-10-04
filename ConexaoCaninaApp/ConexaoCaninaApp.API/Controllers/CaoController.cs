using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace ConexaoCaninaApp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CaoController : ControllerBase
	{
		private readonly ICaoService _caoService;

		public CaoController(ICaoService caoService)
		{
			_caoService = caoService;
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
		public async Task<IActionResult> AdicionarCao(CaoDto caoDto)
		{
			var cao = await _caoService.AdicionarCao(caoDto);
			return CreatedAtAction(nameof(ObterCao), new { id = cao.CaoId }, cao);
		}

		[HttpPut("{id}/aprovar")]
		public async Task<IActionResult> AprovarCao(int id)
		{
			await _caoService.AprovarCao(id);
			return NoContent();
		}

		[HttpPut("editar")]
		public async Task<IActionResult> EditarCao([FromBody] EditarCaoDto editarCaoDto)
		{
			await _caoService.AtualizarCao(editarCaoDto);
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
	}
}
