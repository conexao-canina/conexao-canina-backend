using Microsoft.AspNetCore.Mvc;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Application.ViewModel;
using ConexaoCaninaApp.Application.Requests;
using ConexaoCaninaApp.Application.Dto;

namespace ConexaoCaninaApp.API.Controllers
{
	[Route("api[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly IUsuarioService _usuarioService;

		public UsuarioController(IUsuarioService usuarioService)
		{
			_usuarioService = usuarioService;
		}

		// Get para usuario logado, com favoritos, todos cachorros e sugestoes
		[HttpGet]
		[Route("loggedUser/{email}")]
		public async Task<IActionResult> GetByLoggedUser(string email)
		{
			if (email == null) return BadRequest();

			var result = _usuarioService.GetByLoggedUser(email);
			if (result == null) return NoContent();

			return Ok(new UserViewModel
			{
				UsuarioId = result.UsuarioId,
				Email = result.Email,
				Nome = result.Nome,
				Telefone = result.Telefone,
				IsAdmin = result.IsAdmin,
				Favoritos = result.Favoritos.Select
				(x => new FavoritoViewModel
				{
					FavoritoId = x.FavoritoId,
					Cao = new CaoDetalhesViewModel
					{
						Age= x.Cao.Idade,
						Breed = x.Cao.Raca,
						City = x.Cao.Cidade,
						Description = x.Cao.Descricao,
						DogId = x.Cao.CaoId,
						Gender = x.Cao.Genero.ToString(),
						Name = x.Cao.Nome,
						Size = x.Cao.Tamanho.ToString(),
						State = x.Cao.Estado,
						UniqueCharacteristics = x.Cao.CaracteristicasUnicas,
					},
					Data = x.Data,
				}).ToList(),

				Sugestoes = result.Sugestoes.Select(x => new SugestaoViewModel
				{
					DataEnvio = x.DataEnvio,
					Descricao = x.Descricao,
					Status = x.Status,
					Feedback = x.FeedBack,
					SugestaoId = x.SugestaoId
				}).ToList(),
				Caes = result.Caes.Select(x => new CaoDetalhesViewModel
				{
					Age = x.Idade,
					Breed = x.Raca,
					City = x.Cidade,
					Description = x.Descricao,
					DogId = x.CaoId,
					Gender = x.Genero.ToString(),
					HealthHistories = x.HistoricosDeSaude.Select(y => new HealthHistoryViewModel
					{
						ExamDate = y.DateExame,
						Exam = y.Exame,
						Vaccine = y.Vacina,
						OwnerConsent = y.ConsentimentoDono,
						ConditionsOfHealth = y.CondicoesDeSaude,
						HealthHistoryId = y.HistoricoSaudeId
					}).ToList(),
				}).ToList()
			});
		}

		// Criar junto com o firebase
		[HttpPost]
		public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioRequest request)
		{
			if (request == null) return BadRequest();

			var result = _usuarioService.Create(new CriarUsuarioDTO
			{
				Email = request.Email,
				Nome = request.Nome,
				Telefone = request.Telefone,
				Senha = request.Senha,
			});

			if (!result) return BadRequest();

			return Ok();
		}

		// Excluir Usuario
		[HttpDelete]
		public async Task<IActionResult> RemoverUsuario(Guid id)
		{
			var result = _usuarioService.RemoveUsuario(id);
			if(!result) 
				return BadRequest();

			return Ok();
		}

		// Patch para editar senha
		[HttpPatch]
		public async Task<IActionResult> AlterarSenha(Guid id, string password)
		{
			var result = _usuarioService.AlteraSenha(id, password);
			if(!result) 
				return BadRequest();

			return Ok();
		}
		

		// Adicionar Sugestoes

		// Adicionar Cao
		[HttpPost]
		[Route("{userId}/adicionarCao")]
		public async Task<IActionResult> AdicionarCao(Guid userId, [FromBody] AdicionarCaoRequest request)
		{
			if (request == null) return BadRequest();

			var result = _usuarioService.AddCao(userId, new AdicionarCaoDTO
			{
				CaracteristicasUnicas = request.CaracteristicasUnicas,
				Cidade = request.Cidade,
				Descricao = request.Descricao,
				Estado = request.Estado,
				Fotos = request.Fotos.Select(x => new FotoDTO
				{
					CaminhoArquivo = x.CaminhoArquivo,
					Descricao = x.Descricao,
				}).ToList(),
				Genero = request.Genero,
				Idade = request.Idade,
				Nome = request.Nome,
				Raca = request.Raca,
				Tamanho = request.Tamanho,
			});

			if (!result) return BadRequest();
			return Ok();
		}

		// Remover cao
		[HttpDelete]
		[Route("{userId}/removerCao/{caoId}")]
		public async Task<IActionResult> RemoverCao(Guid userId, Guid caoId)
		{
			var result = _usuarioService.RemoveCao(userId, caoId);
			if (!result) return BadRequest();
			return Ok();
		}

		// Adiocionar cao para favoritos
		[HttpPost]
		[Route("{userId}/adicionarFavoritos/{caoId}")]
		public async Task<IActionResult> AdicionarFavoritos(Guid userId, Guid caoId)
		{
			var result = _usuarioService.AddFavoritos(userId, caoId);
			if (!result) return BadRequest();
			return Ok();
		}

		// Remover cao dos favoritos

		// Adicionar Foto

		// Remover Foto

		// Adicionar Historico de Saude

		// Remover Historico de saude
	}
}
