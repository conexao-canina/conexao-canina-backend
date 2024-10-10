using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConexaoCaninaApp.Domain.Test.Services
{
    public class CaoServiceTests
    {
        private readonly CaoService _caoService;
        private readonly Mock<ICaoRepository> _mockCaoRepository;
        private readonly Mock<INotificacaoService> _mockNotificacaoService;
        private readonly Mock<IFotoRepository> _mockFotoRepository;
        private readonly Mock<IArmazenamentoService> _mockArmazenamentoService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public CaoServiceTests()
        {
            _mockCaoRepository = new Mock<ICaoRepository>();
            _mockNotificacaoService = new Mock<INotificacaoService>();
            _mockFotoRepository = new Mock<IFotoRepository>();
            _mockArmazenamentoService = new Mock<IArmazenamentoService>();
            _mockUserContextService = new Mock<IUserContextService>();

            _caoService = new CaoService(
                _mockCaoRepository.Object, _mockNotificacaoService.Object,
                _mockFotoRepository.Object, _mockArmazenamentoService.Object,
                _mockUserContextService.Object);
        }

		#region Task I
        

		[Fact]
        public async Task ObterPorId_Deve_Retornar_Cao_Quando_Existe()
        {
            // ARRANGE

            var caoId = 1;
            var expectedCao = new Cao
            {
                CaoId = caoId,
                Nome = "Deus Imperador PHG"
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
            .ReturnsAsync(expectedCao);

            // ACT

            var result = await _caoService.ObterPorId(caoId);

            // ASSERT

            Assert.NotNull(result);
            Assert.Equal(caoId, result.CaoId);
            Assert.Equal("Deus Imperador PHG", result.Nome);
        }

        [Fact]
        public async Task ObterPorId_Deve_Retornar_Null_Quando_Cao_Nao_Existe()
        {
            // ARRANGE

            var caoId = 1;

            _mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
           .ReturnsAsync((Cao)null);

            // ACT

            var result = await _caoService.ObterPorId(caoId);

            // ASSERT

            Assert.Null(result);
        }

        [Fact]
        public async Task AdicionarCao_Deve_Chamar_Repositorio_E_Notificar_Administrador()
        {
            // ARRANGE

            var caoDto = new CaoDto
            {
                Nome = "Deus Imperador PHG",
                Raca = "Pastor Alemão",
                Idade = 20,
                Tamanho = TamanhoCao.Grande,
                ProprietarioId = 1
            };

			var moderarPerfilDto = new ModerarPerfilDto
			{
				Observacao = "Perfil precisa de mais detalhes"
			};

			var novoCao = new Cao
            {
                Nome = caoDto.Nome,
                Raca = caoDto.Raca,
                Idade = caoDto.Idade,
                Genero = caoDto.Genero,
                Tamanho = caoDto.Tamanho,
                ProprietarioId = caoDto.ProprietarioId,
                Status = StatusCao.Pendente
            };

			// ACT

			var result = await _caoService.AdicionarCao(caoDto, moderarPerfilDto);

			// ASSERT

			_mockCaoRepository.Verify(repo => repo.Adicionar(It.IsAny<Cao>()), Times.Once);
            _mockNotificacaoService.Verify(notif => notif.EnviarNotificacaoParaAdministrador(It.IsAny<Cao>(), It.IsAny<string>()), Times.Once);
			Assert.NotNull(result);
            Assert.Equal(StatusCao.Pendente, result.Status);
        }

        [Fact]
        public async Task AprovarCao_Deve_Atualizar_Status_Para_Aprovado_E_Notificar_Usuario()
        {
            // ARRANGE

            var caoId = 1;
            var caoExistente = new Cao
            {
                CaoId = caoId,
                Nome = "Deus Imperador PHG",
                Status = StatusCao.Pendente
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
           .ReturnsAsync(caoExistente);

            // ACT

            await _caoService.AprovarCao(caoId);

            // ASSERT

            _mockCaoRepository.Verify(repo => repo.Atualizar(It.IsAny<Cao>()), Times.Once);
            _mockNotificacaoService.Verify(notif => notif.EnviarNotificacaoParaUsuario(It.IsAny<Cao>()), Times.Once);
            Assert.Equal(StatusCao.Aprovado, caoExistente.Status);
        }

        [Fact]
        public async Task EditarPerfil_Deve_Definir_Status_Pendente_E_Notificar_Administrador()
        {
            // ARRANGE
            var editarCaoDto = new EditarCaoDto
            {
                CaoId = 1,
                Idade = 5,
                Descricao = "Lindo, forte e adoravel",
                CaracteristicasUnicas = "Forte"
            };

			var moderarPerfilDto = new ModerarPerfilDto
			{
				Observacao = "Perfil precisa de mais detalhes"
			};

			var caoExistente = new Cao
            {
                CaoId = 1,
                Nome = "General PHG",
                Idade = 3,
                Status = StatusCao.Aprovado
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(editarCaoDto.CaoId))
                .ReturnsAsync(caoExistente);

            // ACT

            await _caoService.AtualizarCao(editarCaoDto, moderarPerfilDto);

            // ASSERT

            _mockCaoRepository.Verify(repo => repo.Atualizar
            (It.Is<Cao>(c => c.Status == StatusCao.Pendente)), Times.Once);
			_mockNotificacaoService.Verify(notif => notif.EnviarNotificacaoParaAdministrador(It.IsAny<Cao>(), It.IsAny<string>()), Times.Once);


		}

		[Fact]
        public async Task VerificarPermissaoEdicao_Deve_Retornar_Verdadeiro_Se_Usuario_For_Proprietario()
        {
            // ARRANGE
            var cao = new Cao
            {
                CaoId = 1,
                Nome = "Comandante General da Ordem PHG",
                ProprietarioId = 1234
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(cao.CaoId))
                .ReturnsAsync(cao);

            // ACT

            var temPermissao = await _caoService.VerificarPerimissaoEdicao(cao.CaoId, 1234);

            // ASSERT

            Assert.True(temPermissao);
        }

        [Fact]
        public async Task VerificarPermissaoEdicao_Deve_Retornar_Falso_Se_Usuario_Nao_For_Proprietario()
        {
            // ARRANGE

            var cao = new Cao
            {
                CaoId = 1,
                Nome = "Comandante General da Ordem PHG",
                ProprietarioId = 1234
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(cao.CaoId))
                .ReturnsAsync(cao);

            // ACT

            var temPermissao = await _caoService.VerificarPerimissaoEdicao(cao.CaoId, 456);

            // ASSERT

            Assert.False(temPermissao);

        }

        [Fact]
        public async Task PublicarCao_Deve_Mudar_Status_Para_Publicado_Se_Estiver_Aprovado()
        {
            // ARRANGE

            var cao = new Cao
            {
                CaoId = 1,
                Nome = "Comandante General da Ordem PHG",
                Status = StatusCao.Aprovado
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(cao.CaoId))
                .ReturnsAsync(cao);

            // ACT

            await _caoService.PublicarCao(cao.CaoId);

            // ASSERT

            _mockCaoRepository.Verify(repo => repo.Atualizar
            (It.Is<Cao>(c => c.Status == StatusCao.Publicado)), Times.Once);

        }

        [Fact]
        public async Task ExcluirCao_Deve_Excluir_Cao_E_Fotos_Associadas()
        {
            var caoId = 1;
            var cao = new Cao
            {
                // ARRANGE
                CaoId = caoId,
                Nome = "Imperador Supremo PHG",
                Fotos = new List<Foto>
                {
                    new Foto { FotoId = 1, CaminhoArquivo = "/uploads/foto1.jpg" },
                    new Foto { FotoId = 2, CaminhoArquivo = "/uploads/foto2.jpg" }
                },
                Proprietario = new Proprietario { Email = "dono@teste.com" }
            };

            _mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

            // ACT

            await _caoService.ExcluirCao(caoId);

            // ASSERT 

            _mockCaoRepository.Verify(r => r.Remover(cao), Times.Once);
            _mockArmazenamentoService.Verify(s => s.ExcluirArquivoAsync("/uploads/foto1.jpg"), Times.Once);
            _mockArmazenamentoService.Verify(s => s.ExcluirArquivoAsync("/uploads/foto2.jpg"), Times.Once);
            _mockNotificacaoService.Verify(n => n.EnviarNotificacaoDeExclusaoParaUsuario
            (cao.Proprietario.Email, cao.Nome,
            "A exclusão do perfil é permanente. Caso deseje retornar,será necessário criar um novo perfil."), Times.Once);
        }

        [Fact]
        public async Task ExcluirCao_Deve_Enviar_Notificacao_Para_Usuario()
        {
            var caoId = 1;
            var cao = new Cao
            {
                // ARRANGE 
                CaoId = caoId,
                Nome = "Imperador Supremo PHG",
                Proprietario = new Proprietario { Email = "dono@teste.com" }
            };

            _mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

            // ACT 

            await _caoService.ExcluirCao(caoId);

            // ASSERT 

            _mockCaoRepository.Verify(r => r.Remover(cao), Times.Once);
            _mockNotificacaoService.Verify(n => n.EnviarNotificacaoDeExclusaoParaUsuario
            (cao.Proprietario.Email, cao.Nome,
            "A exclusão do perfil é permanente. Caso deseje retornar,será necessário criar um novo perfil."), Times.Once);
        }


		#endregion

		#region Task II


		[Fact]
        public async Task ObterDetalhesCao_Deve_Retornar_Informacoes_Completas()
        {
            // ARRANGE 

            var caoId = 1;
            var cao = new Cao
            {
                // ARRANGE
                CaoId = caoId,
                Nome = "Imperador Supremo PHG",
                Raca = "Pastor Alemão",
                Idade = 5,
                Genero = 1,
                CaracteristicasUnicas = "Bruto e forte tal como uma pedra",
                Descricao = "Lindo, belo, amigavel"
            };

            _mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

            // ACT

            var result = await _caoService.ObterPorId(caoId);

            // ASSERT 

            Assert.NotNull(result);
            Assert.Equal(cao.Nome, result.Nome);
            Assert.Equal(cao.Raca, result.Raca);
            Assert.Equal(cao.Idade, result.Idade);
            Assert.Equal(cao.Genero, result.Genero);
            Assert.Equal(cao.CaracteristicasUnicas, result.CaracteristicasUnicas);
            Assert.Equal(cao.Descricao, result.Descricao);

        }

        [Fact]
        public async Task AtualizarInformacoesBasicas_Deve_Atualizar_Cao()
        {
            var caoId = 1;
            var cao = new Cao
            {
                CaoId = caoId,
                Nome = "Comandante General PHG",
                Idade = 4,
                ProprietarioId = 1
            };

            _mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

            var dto = new AtualizarInformacoesBasicasDto
            {
                Nome = "Deus Imperador PHG",
                Idade = 9,
                Raca = "BIG",
                Genero = 1,
                CaracteristicasUnicas = "Forte, bruto mas fofo.."
            };

            _mockUserContextService.Setup(s => s.GetUserId()).Returns("1");

			await _caoService.AtualizarInformacoesBasicas(caoId, dto);

            _mockCaoRepository.Verify(r => r.Atualizar(It.IsAny<Cao>()), Times.Once);
            Assert.Equal("Deus Imperador PHG", cao.Nome);
            Assert.Equal(9, cao.Idade);
		}

        [Fact]
        public async Task AtualizarInformacoesBasicas_Deve_Lancar_Excecao_Se_Usuario_Nao_Tiver_Permissao()
        {
            var caoId = 1;
            var dto = new AtualizarInformacoesBasicasDto
            {
                Nome = "Deus Imperador PHG",
                Idade = 9,
                Raca = "Hec",
                Genero = 1,
                CaracteristicasUnicas = "Lindoooo"
            };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
                .ReturnsAsync(new Cao
                {
                    CaoId = caoId,
                    Nome = "Imperador PHG",
                    ProprietarioId = 1234,
                });

            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => 
            _caoService.AtualizarInformacoesBasicas(caoId, dto));

            Assert.Equal("Você não tem permissão para editar este perfil.", exception.Message);
        }

        [Fact]
        public async Task ExcluirCao_Deve_Lancar_Excecao_Se_Cao_Nao_Existe()
        {
            // ARRANGE

            var caoId = 1123;

            _mockCaoRepository.Setup(repo => repo.ObterPorId(caoId))
                .ReturnsAsync((Cao)null);

            // ACT & ASSERT 

            var exception = await Assert.ThrowsAsync<Exception>(() =>
            _caoService.ExcluirCao(caoId));

            Assert.Equal("Cão não encontrado", exception.Message);

		}

        [Fact]
        public async Task ModerarPerfil_Deve_AprovarCaoEEnviarNotificacao()
        {
            var cao = new Cao
            {
                CaoId = 1,
                Nome = "Deus Imperador PHG",
                Status = StatusCao.Pendente
            };

            var moderarPerfilDto = new ModerarPerfilDto { Aprovado = true };


            _mockCaoRepository.Setup(r => r.ObterPorId(1)).ReturnsAsync(cao);
            _mockNotificacaoService.Setup(n => n.EnviarNotificacaoParaUsuario
            (It.IsAny<Cao>())).Returns(Task.CompletedTask);

            await _caoService.ModerarPerfil(1, moderarPerfilDto);


            Assert.Equal(StatusCao.Aprovado, cao.Status);
            _mockNotificacaoService.Verify(n => n.EnviarNotificacaoParaUsuario(cao), Times.Once);


		}

		[Fact]
		public async Task ModerarPerfil_Deve_RejeitarCaoEEnviarNotificacaoAoAdministrador()
		{
			var cao = new Cao
			{
				CaoId = 1,
				Nome = "Deus Imperador PHG",
				Status = StatusCao.Pendente
			};

			var moderarPerfilDto = new ModerarPerfilDto { Aprovado = false, Observacao = "Faltam fotos." };


			_mockCaoRepository.Setup(r => r.ObterPorId(1)).ReturnsAsync(cao);

			_mockNotificacaoService.Setup(n => n.EnviarNotificacaoParaAdministrador
            (It.IsAny<Cao>(), It.IsAny<string>())).Returns(Task.CompletedTask);


			await _caoService.ModerarPerfil(1, moderarPerfilDto);


            Assert.Equal(StatusCao.Pendente, cao.Status);
            _mockNotificacaoService.Verify(n => n.EnviarNotificacaoParaAdministrador(cao, "Faltam fotos."), Times.Once);
		}


		[Fact]
		public async Task AdicionarLike_DeveAdicionarLikeAoPerfilCao()
		{
			var caoId = 1;
			var usuarioId = 1;

			var cao = new Cao
			{
				CaoId = caoId,
				Likes = new List<Like>(),
				Proprietario = new Proprietario
				{
					Email = "dono@exemplo.com"
				},
				Nome = "NomeDoCao"
			};

			_mockCaoRepository.Setup(repo => repo.ObterPorId(caoId)).ReturnsAsync(cao);


			await _caoService.DarLike(caoId, usuarioId);


			Assert.Contains(cao.Likes, l => l.UsuarioId == usuarioId);
			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoDeLike(cao.Proprietario.Email, cao.Nome), Times.Once);
			_mockCaoRepository.Verify(repo => repo.Atualizar(cao), Times.Once);
		}

		[Fact]
		public async Task RemoverLike_DeveRemoverLikeDoPerfilCao()
		{
			var caoId = 1;
			var usuarioId = 1;
			var like = new Like { UsuarioId = usuarioId };
			var cao = new Cao
			{
				CaoId = caoId,
				Likes = new List<Like> { like },
				Proprietario = new Proprietario
				{
					Email = "dono@exemplo.com"
				},
				Nome = "NomeDoCao"
			};

			_mockCaoRepository.Setup(repo => repo.ObterPorId(caoId)).ReturnsAsync(cao);

			await _caoService.RemoverLike(caoId, usuarioId);

			Assert.DoesNotContain(cao.Likes, l => l.UsuarioId == usuarioId);
			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoDeUnlike(cao.Proprietario.Email, cao.Nome), Times.Once); // Verificar "Unlike"
			_mockCaoRepository.Verify(repo => repo.Atualizar(cao), Times.Once);
		}


		[Fact]
        public async Task AdicionarLike_DeveLancarExcecao_SeLikeJaExistir()
        {
			var caoId = 1;
			var usuarioId = 1;
			var like = new Like { UsuarioId = usuarioId };
			var cao = new Cao { CaoId = caoId, Likes = new List<Like> { like } };

            _mockCaoRepository.Setup(repo => repo.ObterPorId(caoId)).ReturnsAsync(cao);


			await Assert.ThrowsAsync<InvalidOperationException>
                (() => _caoService.DarLike(caoId, usuarioId));
		}

		[Fact]
		public async Task DarLike_DeveDispararNotificacao()
		{
			var caoId = 1;
			var usuarioId = 1;
			var cao = new Cao
			{
				CaoId = caoId,
				Likes = new List<Like>(),
				Proprietario = new Proprietario
				{
					Email = "dono@exemplo.com"
				},
				Nome = "NomeDoCao"
			};
			_mockCaoRepository.Setup(r => r.ObterPorId(caoId)).ReturnsAsync(cao);

			await _caoService.DarLike(caoId, usuarioId);

			_mockNotificacaoService.Verify(n => n.EnviarNotificacaoDeLike(cao.Proprietario.Email, cao.Nome), Times.Once);
			_mockCaoRepository.Verify(r => r.Atualizar(cao), Times.Once);
		}


		#endregion
	}
}

