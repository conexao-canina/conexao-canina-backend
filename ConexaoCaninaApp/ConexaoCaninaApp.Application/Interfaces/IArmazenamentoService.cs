using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface IArmazenamentoService
	{
		Task<string> SalvarArquivoAsync(IFormFile arquivo);
		Task ExcluirArquivoAsync(string caminhoArquivo);

	}
}
