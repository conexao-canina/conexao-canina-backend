using Microsoft.AspNetCore.Mvc;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Application.ViewModel;
using ConexaoCaninaApp.Application.Requests;
using ConexaoCaninaApp.Application.Dto;

namespace ConexaoCaninaApp.API.Controllers
{
	[Route("api/[controller]")]
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
						Age = x.Cao.Idade,
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
			if (!result)
				return BadRequest();

			return Ok();
		}

		// Patch para editar senha
		[HttpPatch]
		public async Task<IActionResult> AlterarSenha(Guid id, string password)
		{
			var result = _usuarioService.AlteraSenha(id, password);
			if (!result)
				return BadRequest();

			return Ok();
		}


		// Adicionar Sugestoes
		[HttpPost]
		[Route("{userId}/adicionarSugestao")]
		public async Task<IActionResult> AdicionarSugestao(Guid userId, [FromBody] SugestaoDTO request)
		{
			if (request == null)
				return BadRequest();

			var result = _usuarioService.AddSugestao(userId, new SugestaoDTO
			{
				Descricao = request.Descricao,
			});

			if (!result)
				return BadRequest();

			return Ok();
		}

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
		[HttpDelete]
		[Route("{userId}/removerFavoritos/{caoId}")]
		public async Task<IActionResult> RemoverFavoritos(Guid userId, Guid caoId)
		{
			var result = _usuarioService.RemoveFavoritos(userId, caoId);
			if (!result)
				return BadRequest();

			return Ok();
		}

		// Adicionar Foto
		[HttpPost]
		[Route("{userId}/adicionarFoto/{caoId}")]
		public async Task<IActionResult> AdicionarFoto(Guid userId, Guid caoId, [FromBody] FotoDTO foto)
		{
			if (foto == null)
				return BadRequest();

			var result = _usuarioService.AddFoto(userId, caoId, foto);

			if (!result)
				return BadRequest();

			return Ok();
		}

		// Remover Foto
		[HttpPost]
		[Route("{userId}/removerFoto/{caoId}/{fotoId}")]
		public async Task<IActionResult> RemoverFoto(Guid userId, Guid caoId, Guid fotoId)
		{
			var result = _usuarioService.RemoveFoto(userId, caoId, fotoId);
			if (!result)
				return BadRequest();

			return Ok();
		}

		// Adicionar Historico de Saude
		[HttpPost]
		[Route("{userId}/adicionarHistoricoDeSaude/{caoId}")]
		public async Task<IActionResult> AdicionarHistoricoDeSaude(Guid userId, Guid caoId, [FromBody] HistoricoDeSaudeDto historico)
		{
            if (historico == null)
                return BadRequest();

            var result = _usuarioService.AddHistoricoDeSaude(userId, caoId, historico);

            if (!result)
                return BadRequest();

            return Ok();
        }

		// Remover Historico de saude
		[HttpDelete]
        [Route("{userId}/removerHistoricoDeSaude/{caoId}/{historicoId}")]
        public async Task<IActionResult> RemoverHistoricoDeSaude(Guid userId, Guid caoId, Guid historicoId)
        {
            var result = _usuarioService.RemoveHistoricoDeSaude(userId, caoId, historicoId);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
