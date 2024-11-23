using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.ViewModel
{
	public class UserViewModel
	{
		public Guid UsuarioId { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public bool IsAdmin { get; set; }
		public ICollection<CaoDetalhesViewModel> Caes {  get; set; } 
		= new List<CaoDetalhesViewModel>();
		public ICollection<FavoritoViewModel> Favoritos { get; set; }
		= new List<FavoritoViewModel>();
		public ICollection<SugestaoViewModel> Sugestoes { get; set; }
		= new List<SugestaoViewModel>();

	}
}
