//using ConexaoCaninaApp.Application.Services;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace ConexaoCaninaApp.Domain.Test.Services
//{
//    public class ArmazenamentoServiceTests
//    {

//        private readonly ArmazenamentoLocalService _armazenamentoService;
//        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;

//        public ArmazenamentoServiceTests()
//        {
//            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
//            _mockWebHostEnvironment.Setup(env => env.WebRootPath).Returns(Directory.GetCurrentDirectory());
//            _armazenamentoService = new ArmazenamentoLocalService(_mockWebHostEnvironment.Object);

//        }

//        [Fact]
//        public async Task SalvarArquivo_DeveSalvarArquivoNoCaminhoCorreto()
//        {
//            // ARRANGE

//            var arquivoMock = CriarArquivoMock("imagem_teste.jpg");
//            var caminhoUpload = Path.Combine(_mockWebHostEnvironment.Object.WebRootPath, "uploads");

//            if (Directory.Exists(caminhoUpload))
//            {
//                Directory.Delete(caminhoUpload, true); // limpa o diretorio
//            }

//            // ACT

//            var caminhoArquivo = await _armazenamentoService.SalvarArquivoAsync(arquivoMock, 1);

//            // ASSERT

//            var caminhoEsperado = Path.Combine("uploads", caminhoArquivo);
//            Assert.Equal(caminhoEsperado, caminhoArquivo);
//        }

//        private IFormFile CriarArquivoMock(string nomeArquivo)
//        {
//            var content = "Fake content";
//            var fileName = nomeArquivo;
//            var stream = new MemoryStream();
//            var writer = new StreamWriter(stream);
//            writer.Write(content);
//            writer.Flush();
//            stream.Position = 0;

//            return new FormFile(stream, 0, stream.Length, "id_form", fileName)
//            {
//                Headers = new HeaderDictionary(),
//                ContentType = "image/jpeg"
//            };
//        }
//    }
//}
