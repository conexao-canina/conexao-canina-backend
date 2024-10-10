using ConexaoCaninaApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface ISugestaoService
	{
		Task EnviarSugestaoAsync(SugestaoDto sugestaoDto);
		Task<List<SugestaoDto>> ObterSugestoesPorUsuarioAsync(int usuarioId);
	}
}
