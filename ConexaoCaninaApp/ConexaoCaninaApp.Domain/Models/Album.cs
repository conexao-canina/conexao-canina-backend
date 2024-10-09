using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class Album
	{
		public int AlbumId { get; set; }	
		public string Nome { get; set; }
		public string Descricao { get; set; }

		public int ProprietarioId { get; set; }
		public ICollection<Foto> Fotos { get; set; } = new List<Foto>();

		public string Privacidade { get; set; }
		public ICollection<Usuario> UsuariosPermitidos { get; set; }
	}
}
