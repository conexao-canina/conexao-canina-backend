using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


// O teste simula o upload de fotos e verifica se o rep de fotos e o serviço de 
// armazenamento sao chamados de forma correta
namespace ConexaoCaninaApp.Domain.Test
{
	public class FotoServiceTests
	{
		private readonly FotoService _fotoService;
		private readonly Mock<IFotoRepository> _mockFotoRepository;
		private readonly Mock<IArmazenamentoService> _mockArmazenamentoService;

		public FotoServiceTests()
		{
			_mockFotoRepository = new Mock<IFotoRepository>();
			_mockArmazenamentoService = new Mock<IArmazenamentoService>();
			_fotoService = new FotoService(_mockFotoRepository.Object, _mockArmazenamentoService.Object);
		}

		[Fact]
		public async Task UploadFotos_Deve_Salvar_Arquivos_E_Adicionar_Fotos()
		{
			// arrange
			var arquivos = new List<IFormFile>
			{
				CriarArquivoMock("foto1.jpg"),
				CriarArquivoMock("foto2.jpg")
			};

			_mockArmazenamentoService.Setup(s => s.SalvarArquivoAsync(It.IsAny<IFormFile>()))
				.ReturnsAsync("/uploads/foto1.jpg");

			_mockFotoRepository.Setup(r => r.ObterProximaOrdemAsync(It.IsAny<int>()))
				.ReturnsAsync(1);


			// act

			var result = await _fotoService.UploadFotosAsync(arquivos, 1);

			// assert

			_mockFotoRepository.Verify(r => r.Adicionar(It.IsAny<Foto>()), Times.Exactly(arquivos.Count));
			Assert.Equal(2, result.Count());

		}

		private IFormFile CriarArquivoMock(string nomeArquivo)
		{
			var content = "Fake content";
			var fileName = nomeArquivo;
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(content);
			writer.Flush();
			stream.Position = 0;

			return new FormFile(stream, 0, stream.Length, "id_from_form", fileName)
			{
				Headers = new HeaderDictionary(),
				ContentType = "image/jpeg"
			};
		}
	}
}
