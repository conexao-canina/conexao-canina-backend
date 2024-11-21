using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class Usuario
	{
		private Usuario() { }

		public Usuario(string nome, string email, 
			string senha, bool isAdmin,
			string telefone)
		{
			UsuarioId = Guid.NewGuid();
			Nome = nome;
			Senha = senha;
			Email = email;
			Telefone = telefone;
			IsAdmin = isAdmin;

		}

		public Guid UsuarioId { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Senha { get; set; }
		public bool IsAdmin { get; set; }
		public string Telefone { get; set; }	
		public ICollection<Cao> Caes { get; set; } = new List<Cao>();
		public ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();
		public ICollection<Sugestao> Sugestoes { get; set;} = new List<Sugestao>();

		public void AddCao(Cao cao)
		{
			Caes.Add(cao);
		}

		public void RemoveCao(Cao cao)
		{
			Caes.Remove(cao);
		}

		public void AddFavorito(Favorito like)
		{
			Favoritos.Add(like);
		}

		public void RemoveFavorito(Favorito like)
		{
			Favoritos.Remove(like);
		}

		public void AddSugestoes(Sugestao sugestao)
		{
			Sugestoes.Add(sugestao);
		}
	}
}
