using Microsoft.AspNetCore.Mvc;
using ConexaoCaninaApp.Application.Services;

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
	}
}
