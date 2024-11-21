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
		bool AddCao(AdicionarCaoDTO request);
	}

	public class UsuarioService : IUsuarioService
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public UsuarioService(IUsuarioRepository usuarioRepository)
		{
			_usuarioRepository = usuarioRepository ?? throw new 
				ArgumentNullException(nameof(usuarioRepository));
		}

		public bool AddCao(AdicionarCaoDTO request)
		{
			var user = _usuarioRepository.GetById(request.UsuarioId);
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
	}
}
