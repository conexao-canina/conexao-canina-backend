using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface IFotoService
	{
		Task<IEnumerable<FotoDto>> UploadFotosAsync(List<IFormFile> arquivos, int caoId, int albumId);
		Task ExcluirFotoAsync(int fotoId);
		Task ReordenarFotosAsync(IEnumerable<FotoDto> fotos);
		Task<IEnumerable<FotoDto>> ObterFotosPorCaoId(int caoId);	
	}
}
