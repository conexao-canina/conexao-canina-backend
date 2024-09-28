using ConexaoCaninaApp.Application.Dto;
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
		Task<IEnumerable<FotoDto>> UploadFotosAsync(List<IFormFile> arquivos, int caoId);
		Task ExcluirFotoAsync(int fotoId);
		Task ReordenarFotosAsync(IEnumerable<FotoDto> fotos);
	}
}
