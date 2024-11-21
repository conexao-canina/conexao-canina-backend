using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class Favorito
	{
		private Favorito() { }

		public Favorito(Guid caoId, DateTime data)
		{
			FavoritoId = Guid.NewGuid();
			_caoId = caoId;
			Data = data;
		}

		public Guid FavoritoId { get; set; }
		public Cao Cao { get; set; }
		public DateTime Data { get; set; } = DateTime.Now;
		private Guid _caoId;
	}
}
