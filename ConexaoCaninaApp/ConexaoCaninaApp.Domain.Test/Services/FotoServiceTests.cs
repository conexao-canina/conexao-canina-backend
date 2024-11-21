﻿//using ConexaoCaninaApp.Application.Dto;
//using ConexaoCaninaApp.Application.Interfaces;
//using ConexaoCaninaApp.Application.Services;
//using ConexaoCaninaApp.Domain.Models;
//using ConexaoCaninaApp.Infra.Data.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;


//// O teste simula o upload de fotos e verifica se o rep de fotos e o serviço de 
//// armazenamento sao chamados de forma correta
//namespace ConexaoCaninaApp.Domain.Test.Services
//{
//    public class FotoServiceTests : IClassFixture<WebApplicationFactory<Program>>
//	{
//        private readonly WebApplicationFactory<Program> _factory;
//        private readonly FotoService _fotoService;
//        private readonly Mock<IFotoRepository> _mockFotoRepository;
//        private readonly Mock<IArmazenamentoService> _mockArmazenamentoService;
//        private readonly Mock<ICaoRepository> _mockCaoRepository;


//        public FotoServiceTests(WebApplicationFactory<Program> factory)
//        {
//            _factory = factory;
//            _mockFotoRepository = new Mock<IFotoRepository>();
//            _mockArmazenamentoService = new Mock<IArmazenamentoService>();
//            _mockCaoRepository = new Mock<ICaoRepository>();

//            _fotoService = new FotoService(_mockFotoRepository.Object, _mockArmazenamentoService.Object, _mockCaoRepository.Object);
//        }

//        [Fact]
//        public async Task UploadFotos_Deve_Salvar_Arquivos_E_Adicionar_Fotos()
//        {
//            // arrange
//            var arquivos = new List<IFormFile>
//            {
//                CriarArquivoMock("foto1.jpg"),
//                CriarArquivoMock("foto2.jpg")
//            };


//            _mockCaoRepository.Setup(r => r.ObterPorId(It.IsAny<int>()))
//                .ReturnsAsync(new Cao { CaoId = 1, ProprietarioId = 1 });
//            _mockArmazenamentoService.Setup(s => s.SalvarArquivoAsync(It.IsAny<IFormFile>(), It.IsAny<int>()))
//                .ReturnsAsync("/uploads/foto1.jpg");

//            _mockFotoRepository.Setup(r => r.ObterProximaOrdemAsync(It.IsAny<int>()))
//                .ReturnsAsync(1);


//            // act

//            var result = await _fotoService.UploadFotosAsync(arquivos, 1, 1);

//            // assert

//            _mockFotoRepository.Verify(r => r.Adicionar(It.IsAny<Foto>()), Times.Exactly(arquivos.Count));
//            Assert.Equal(2, result.Count());

//        }

//        [Fact]
//        public async Task ObterGaleriaFotos_Deve_Retornar_Fotos_Quando_Existirem()
//        {
//            // ARRANGE

//            var caoId = 1;
//            var fotosEsperadas = new List<Foto>
//            {
//                new Foto { FotoId = 1, CaminhoArquivo = "/uploads/foto1.jpg", CaoId = caoId },
//                                new Foto { FotoId = 2, CaminhoArquivo = "/uploads/foto2.jpg", CaoId = caoId }
//            };

//            _mockFotoRepository.Setup(r => r.ObterFotosPorCaoId(caoId)).ReturnsAsync(fotosEsperadas);

//            // ACT

//            var result = await _fotoService.ObterFotosPorCaoId(caoId);

//            // ASSERT 

//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count());
//            _mockFotoRepository.Verify(r => r.ObterFotosPorCaoId(caoId), Times.Once);
//        }

//        [Fact]
//        public async Task ObterGaleriaFotos_Deve_Retornar_Lista_Vazia_Se_Nao_Existirem_Fotos()
//        {
//            // ARRANGE

//            var caoId = 1;
//            var fotosEsperadas = new List<Foto>();

//            _mockFotoRepository.Setup(r => r.ObterFotosPorCaoId(caoId)).ReturnsAsync(fotosEsperadas);

//            // ACT 

//            var result = await _fotoService.ObterFotosPorCaoId(caoId);

//            // ASSERT

//            Assert.NotNull(result);
//            Assert.Empty(result);
//        }

//        [Fact]
//        public async Task ExcluirFotoAsync_Deve_ExcluirFoto()
//        {
//            // ARRANGE

//            var fotoId = 1;
//            var foto = new Foto
//            {
//                FotoId = fotoId,
//                CaminhoArquivo = "/uploads/foto.jpg"
//			};

//            _mockFotoRepository.Setup(r => r.ObterPorId(fotoId)).ReturnsAsync(foto);    

//            // ACT

//            await _fotoService.ExcluirFotoAsync(fotoId);

//            // ASSERT

//            _mockFotoRepository.Verify(r => r.Remover(foto), Times.Once);
//			_mockArmazenamentoService.Verify(s => 
//            s.ExcluirArquivoAsync(foto.CaminhoArquivo), Times.Once);

//		}

//        [Fact] async Task ReordenarFotosAsync_Deve_Atualizar_Ordem()
//        {
//            // ARRANGE 

//            var fotos = new List<FotoDto>
//            {
//                new FotoDto { FotoId= 1, Ordem = 2 },
//				new FotoDto { FotoId= 2, Ordem = 1 }
//			};

//            var foto1 = new Foto { FotoId = 1, Ordem = 1 };
//            var foto2 = new Foto { FotoId = 2, Ordem = 2 };

//            _mockFotoRepository.Setup(repo => repo.ObterPorId(1)).ReturnsAsync(foto1);
//            _mockFotoRepository.Setup(repo => repo.ObterPorId(2)).ReturnsAsync(foto2);


//			// ACT

//            await _fotoService.ReordenarFotosAsync(fotos);

//            // ASSERT

//            Assert.Equal(2, foto1.Ordem);
//            Assert.Equal(1, foto2.Ordem);


//            _mockFotoRepository.Verify(repo => repo.Atualizar(foto1), Times.Once);
//			_mockFotoRepository.Verify(repo => repo.Atualizar(foto2), Times.Once);
//		}

//		[Fact]
//		async Task ObterFotosPorCaoId_Deve_Retornar_Fotos()
//		{
//			// ARRANGE 

//			var fotos = new List<Foto>
//			{
//				new Foto { FotoId= 1, CaminhoArquivo = "/path/to/file1.jpg"},
//				new Foto { FotoId= 2, CaminhoArquivo = "/path/to/file2.jpg"},
//			};

//            _mockFotoRepository.Setup(repo => repo.ObterFotosPorCaoId(1)).ReturnsAsync(fotos);
//            // ACT

//            var result = await _fotoService.ObterFotosPorCaoId(1);

//			// ASSERT

//            Assert.Equal(2, result.Count());
//			Assert.Equal("/path/to/file1.jpg", result.First().CaminhoArquivo);
//		}

//        [Fact]
//        public async Task AtualizarOrdemEAdicionarFotos_Deve_AtualizarOrdemDasFotosExistentes_E_AdicionarNovasFotos()
//        {
//            var novasFotos = new List<IFormFile>
//            {
//                CriarArquivoMock("foto1.jpg"),
//                CriarArquivoMock("foto2.jpg")
//            };

//            var fotosExistentes = new List<FotoDto>
//            {
//                new FotoDto { FotoId = 1, Ordem = 1 },
//                new FotoDto { FotoId = 2, Ordem = 2 }
//            };

//            _mockFotoRepository.Setup(r => r.ObterPorId(It.IsAny<int>()))
//                .ReturnsAsync(new Foto { FotoId = 1, Ordem = 1 });

//            _mockFotoRepository.Setup(r => r.ObterProximaOrdemAsync(It.IsAny<int>()))
//                .ReturnsAsync(3);


//            await _fotoService.AtualizarOrdemEAdicionarFotosAsync(1, novasFotos, fotosExistentes);

//            _mockFotoRepository.Verify(r => r.Atualizar(It.IsAny<Foto>()), Times.Exactly(fotosExistentes.Count));
//            _mockFotoRepository.Verify(r => r.Adicionar(It.IsAny<Foto>()), Times.Exactly(novasFotos.Count));

//		}

//        [Fact]
//        public async Task RemoverFotoDoAlbum_Deve_RemoverFotoDoBancoDeDados_E_ExcluirArquivoFisico()
//        {
//            var fotoId = 1;
//            var foto = new Foto { FotoId = fotoId, CaminhoArquivo = "/uploads/foto1.jpg" };

//            _mockFotoRepository.Setup(r => r.ObterPorId(fotoId)).ReturnsAsync(foto);

//            await _fotoService.RemoverFotoDoAlbumAsync(fotoId);

//            _mockFotoRepository.Verify(r => r.Remover(foto), Times.Once);
//			_mockArmazenamentoService.Verify(s => s.ExcluirArquivoAsync(foto.CaminhoArquivo), Times.Once);

//		}



//		private IFormFile CriarArquivoMock(string nomeArquivo)
//        {
//            var content = "Fake content";
//            var fileName = nomeArquivo;
//            var stream = new MemoryStream();
//            var writer = new StreamWriter(stream);
//            writer.Write(content);
//            writer.Flush();
//            stream.Position = 0;

//            return new FormFile(stream, 0, stream.Length, "id_from_form", fileName)
//            {
//                Headers = new HeaderDictionary(),
//                ContentType = "image/jpeg"
//            };
//        }
//    }
//}
