using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class Like
	{
		public int LikeId { get; set; }
		public int CaoId { get; set; }
		public int UsuarioId { get; set; }
		public DateTime DataLike { get; set; } = DateTime.Now;
		public bool IsLike { get; set; }
		public Cao Cao { get; set; }
	}
}
