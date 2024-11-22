using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class UserDTO
	{
		public Guid UsuarioId { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public bool IsAdmin { get; set; }
		public ICollection<CaoDTO> Caes { get; set; } = new List<CaoDTO>();
		public ICollection<FavoritoDTO> Favoritos { get; set; } = new List<FavoritoDTO>();
		public ICollection<SugestaoDTO> Sugestoes { get; set; } = new List<SugestaoDTO>();

	}
}
