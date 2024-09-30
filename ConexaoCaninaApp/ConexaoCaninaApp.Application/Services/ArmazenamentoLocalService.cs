using ConexaoCaninaApp.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class ArmazenamentoLocalService : IArmazenamentoService
	{
		private readonly IWebHostEnvironment _environment;

		public ArmazenamentoLocalService(IWebHostEnvironment environment)
		{
			_environment = environment;
		}

		public async Task<string> SalvarArquivoAsync(IFormFile arquivo, int proprietarioId)
		{
			if (arquivo == null || arquivo.Length == 0)
			
				throw new ArgumentNullException("Arquivo invalido...");

			var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png" };
			var extensaoArquivo = Path.GetExtension(arquivo.FileName).ToLower();

			if (!extensoesPermitidas.Contains(extensaoArquivo))
			{
				throw new ArgumentException("Tipo de arquivo nao permitido.");
			}

			var caminhoUpload = Path.Combine
				(_environment.WebRootPath, "uploads", "proprietario", proprietarioId.ToString());

			if (!Directory.Exists(caminhoUpload))
			{
				Directory.CreateDirectory(caminhoUpload);
			}

			var nomeArquivo = $"{Guid.NewGuid()}_{arquivo.FileName}"; 
			var caminhoCompleto = Path.Combine(caminhoUpload, nomeArquivo);

			using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
			{
				await arquivo.CopyToAsync(stream);
			}

			return $"/uploads/proprietario/{proprietarioId}/{nomeArquivo}"; // retorna o caminho para armazenar no banco
		}

		public Task ExcluirArquivoAsync(string caminhoArquivo)
		{
			var caminhoCompleto = Path.Combine(_environment.WebRootPath, caminhoArquivo.TrimStart('/'));

			if (File.Exists(caminhoCompleto))
			{
				File.Delete(caminhoCompleto);
			}

			return Task.CompletedTask;
		}
	}
}
