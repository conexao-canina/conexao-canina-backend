using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class CaoService : ICaoService
	{
		private readonly ICaoRepository _caoRepository;
		private readonly INotificacaoService _notificacaoService;
		private readonly IFotoRepository _fotoRepository;
		private readonly IArmazenamentoService _armazenamentoService;
		private readonly IUserContextService _userContextService;

		public CaoService(ICaoRepository caoRepository, INotificacaoService notificacaoService,
			IFotoRepository fotoRepository, IArmazenamentoService armazenamentoService,
			IUserContextService userContextService)
		{
			_caoRepository = caoRepository;
			_notificacaoService = notificacaoService;
			_fotoRepository = fotoRepository;
			_armazenamentoService = armazenamentoService;
			_userContextService = userContextService;
		}

		public async Task<Cao> AdicionarCao(CaoDto caoDto, ModerarPerfilDto moderarPerfilDto)
		{
			var cao = new Cao
			{
				Nome = caoDto.Nome,
				Raca = caoDto.Raca,
				Idade = caoDto.Idade,
				Descricao = caoDto.Descricao,
				Genero = caoDto.Genero,
				Tamanho = caoDto.Tamanho,
				CaracteristicasUnicas = caoDto.CaracteristicasUnicas,
				ProprietarioId = caoDto.ProprietarioId,
				Status = StatusCao.Pendente,
			};

			await _caoRepository.Adicionar(cao);
			await _notificacaoService.EnviarNotificacaoParaAdministrador(cao, moderarPerfilDto.Observacao);

			return cao;
		}

		public async Task AprovarCao(int caoId)
		{
			var cao = await _caoRepository.ObterPorId(caoId);
			cao.Status = StatusCao.Aprovado;
			await _caoRepository.Atualizar(cao);

			await _notificacaoService.EnviarNotificacaoParaUsuario(cao);
		}

		public async Task<Cao> ObterPorId(int id)
		{
			return await _caoRepository.ObterPorId(id);
		}

		public async Task AtualizarCao(EditarCaoDto editarCaoDto, ModerarPerfilDto moderarPerfilDto)
		{
			var cao = await _caoRepository.ObterPorId(editarCaoDto.CaoId);

			if (cao !=  null)
			{
				cao.Idade = editarCaoDto.Idade;
				cao.Descricao = editarCaoDto.Descricao;
				cao.CaracteristicasUnicas = editarCaoDto.CaracteristicasUnicas;

				cao.Status = StatusCao.Pendente;

				await _caoRepository.Atualizar(cao);

				await _notificacaoService.EnviarNotificacaoParaAdministrador(cao, moderarPerfilDto.Observacao);
			}
		}

		public async Task AtualizarInformacoesBasicas(int caoId, AtualizarInformacoesBasicasDto dto)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			var userId = _userContextService.GetUserId();

			if (userId != cao.ProprietarioId.ToString())
			{
				throw new UnauthorizedAccessException("Você não tem permissão para editar este perfil.");
			}

			cao.Nome = dto.Nome;
			cao.Idade = dto.Idade;
			cao.Raca = dto.Raca;
			cao.Genero = dto.Genero;
			cao.CaracteristicasUnicas = dto.CaracteristicasUnicas;

			await _caoRepository.Atualizar(cao);
			
		}

		public async Task PublicarCao(int caoId)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao != null && cao.Status == StatusCao.Aprovado)
			{
				cao.Status = StatusCao.Publicado;
				await _caoRepository.Atualizar(cao);
			}
		}

		public async Task<bool> VerificarPerimissaoEdicao(int caoId, int usuarioId)
		{
			var cao = await _caoRepository.ObterPorId(caoId);
			if (cao == null)
			{
				return false;
			}

			return cao.ProprietarioId == usuarioId; // para que apenas o proprietarioId edite
		}

		public async Task ExcluirCao(int id)
		{
			var cao = await _caoRepository.ObterPorId(id);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			if (cao.Fotos != null)
			{
				foreach (var foto in cao.Fotos)
				{
					await _fotoRepository.Remover(foto);
					await _armazenamentoService.ExcluirArquivoAsync(foto.CaminhoArquivo);
				}
			}

			await _caoRepository.Remover(cao);

			await _notificacaoService.EnviarNotificacaoDeExclusaoParaUsuario
				(cao.Proprietario.Email, cao.Nome,
				"A exclusão do perfil é permanente. Caso deseje retornar,será necessário criar um novo perfil."
				);
		}

		public async Task ModerarPerfil(int caoId, ModerarPerfilDto moderarPerfilDto)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			if (moderarPerfilDto.Aprovado)
			{
				cao.Status = StatusCao.Aprovado;

				await _notificacaoService.EnviarNotificacaoParaUsuario(cao);
			}
			else
			{
				cao.Status = StatusCao.Pendente;


				await _notificacaoService.EnviarNotificacaoParaAdministrador(cao, moderarPerfilDto.Observacao);
			}

			await _caoRepository.Atualizar(cao);
		}

		public async Task DarLike(int caoId, int usuarioId)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			if (cao.Likes.Any(l => l.UsuarioId == usuarioId))
			{
				throw new InvalidOperationException("Este usuário já curtiu este perfil.");
			}

			if (!cao.Likes.Any(l => l.UsuarioId == usuarioId))
			{
				var like = new Like { CaoId = caoId, UsuarioId = usuarioId };
				cao.Likes.Add(like);

				await _notificacaoService.EnviarNotificacaoDeLike(cao.Proprietario.Email, cao.Nome);
				await _caoRepository.Atualizar(cao);
			}

		}


		public async Task RemoverLike(int caoId, int usuarioId)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			var like = cao.Likes.FirstOrDefault(l => l.UsuarioId == usuarioId);

			if (like != null)
			{
				cao.Likes.Remove(like);


				await _notificacaoService.EnviarNotificacaoDeUnlike(cao.Proprietario.Email, cao.Nome);
				await _caoRepository.Atualizar(cao);
			}
		}
	}
}
