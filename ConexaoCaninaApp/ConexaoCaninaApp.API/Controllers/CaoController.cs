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

		public CaoController(ICaoService caoService)
		{
			_caoService = caoService;
		}
	}
}
