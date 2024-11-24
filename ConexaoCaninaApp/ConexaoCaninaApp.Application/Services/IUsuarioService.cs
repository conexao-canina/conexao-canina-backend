using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public interface IUsuarioService
	{
		bool AddCao(Guid userId, AdicionarCaoDTO request);
		bool RemoveCao(Guid userId, Guid caoId);
		bool RemoveUsuario(Guid userId);
		bool AddFavoritos(Guid userId, Guid caoId);
		bool Create(CriarUsuarioDTO request);
		bool AlteraSenha(Guid userId, string password);
		UserDTO GetByLoggedUser(string email);
	}

	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly ICaoRepository _caoRepository;

		public UsuarioService(IUsuarioRepository usuarioRepository, ICaoRepository caoRepository)
		{
			_usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
			_caoRepository = caoRepository ?? throw new ArgumentNullException(nameof(caoRepository));
		}

		public bool AddCao(Guid userId,AdicionarCaoDTO request)
		{
			var user = _usuarioRepository.GetById(userId);
			if (user == null) return false;

			var cao = new Cao(request.Cidade, request.Estado, request.Nome,
				request.Descricao, request.Raca, request.Idade, request.Tamanho, request.Genero,
				request.CaracteristicasUnicas,
				request.Fotos.Select(
					x => new Foto(
						x.CaminhoArquivo, x.Descricao)).ToList());
			user.AddCao(cao);
			_usuarioRepository.SaveChanges();

			return true;
		}

		public bool AddFavoritos(Guid userId, Guid caoId)
		{
			var user = _usuarioRepository.GetById(userId);
			if (user == null) return false;

			var cao = _caoRepository.GetById(caoId);
			if (cao == null) return false;

			user.AddFavorito(cao);
			_usuarioRepository.SaveChanges();
			return true;
		}

        public bool AlteraSenha(Guid userId, string password)
        {
            var user = _usuarioRepository.GetById(userId);
			if (user == null) 
				return false;

			user.Senha = password;

			_usuarioRepository.SaveChanges();

			return true;
        }

        public bool Create(CriarUsuarioDTO request)
		{
			if (request == null) return false;

			var user = new Usuario(request.Nome, request.Email,
				request.Senha, request.Telefone);
			_usuarioRepository.Add(user);
			_usuarioRepository.SaveChanges();

			return true;
		}

		public UserDTO GetByLoggedUser(string email)
		{
			var user = _usuarioRepository.GetByEmail(email);
			if (user == null) return null;

			return new UserDTO
			{
				UsuarioId = user.UsuarioId,
				Email = email,
				Nome = user.Nome,
				Telefone = user.Telefone,
				IsAdmin = user.IsAdmin,
				Favoritos = user.Favoritos.Select(x => new FavoritoDTO
				{
					Cao = new CaoDTO
					{
						CaoId = x.Cao.CaoId,
						CaracteristicasUnicas = x.Cao.CaracteristicasUnicas,
						Cidade = x.Cao.Cidade,
						Estado = x.Cao.Estado,
						Fotos = x.Cao.Fotos.Select(f => new FotoDTO
						{
							CaminhoArquivo = f.CaminhoArquivo,
							Descricao = f.Descricao
						}).ToList(),
						Genero = x.Cao.Genero,
						Idade = x.Cao.Idade,
						Nome = x.Cao.Nome,
						Raca = x.Cao.Raca,
						Tamanho = x.Cao.Tamanho
					}
				}).ToList(),
				Caes = user.Caes.Select(x => new CaoDTO
				{
					CaoId = x.CaoId,
					CaracteristicasUnicas = x.CaracteristicasUnicas,
					Cidade = x.Cidade,
					Descricao = x.Estado,
					Fotos = x.Fotos.Select(f => new FotoDTO
					{
						CaminhoArquivo = f.CaminhoArquivo,
						Descricao = f.Descricao
					}).ToList(),
					Genero = x.Genero,
					Idade = x.Idade,
					Nome = x.Nome,
					HistoricosDeSaude = x.HistoricosDeSaude.Select(h => new HistoricoDeSaudeDto
					{
						CondicoesDeSaude = h.CondicoesDeSaude,
						ConsentimentoDono = h.ConsentimentoDono,
						DateExame = h.DataExame,
						Exame = h.Exame,
						HistoricoSaudeId = h.HistoricoSaudeId,
						Vacina = h.Vacina
					}).ToList(),
				}).ToList(),
				Sugestoes = user.Sugestoes.Select(x => new SugestaoDTO
				{
					DataEnvio = x.DataEnvio,
					Descricao = x.Descricao,
					SugestaoId = x.SugestaoId,
					FeedBack = x.FeedBack,
					Status = x.Status,
				}).ToList()
			};
		} 

		public bool RemoveCao(Guid userId, Guid caoId)
		{
			var user = _usuarioRepository.GetById(userId);
			if (user == null) return false;

			var cao = _caoRepository.GetById(caoId);
			if (user == null) return false;

			user.RemoveCao(cao);
			_caoRepository.Delete(cao);

			_usuarioRepository.SaveChanges();

			return true;
		}

		public bool RemoveUsuario(Guid userId)
		{
			var user = _usuarioRepository.GetById(userId);
			if (user == null)
				return false;

			_usuarioRepository.Delete(user);

			_usuarioRepository.SaveChanges();

			return true;
		}
	}
}
