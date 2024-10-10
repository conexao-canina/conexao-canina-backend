using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class LikeDto
	{
		public int CaoId { get; set; }
		public int UsuarioId { get; set; }
		public DateTime DataLike { get; set; }
		public bool IsLike { get; set; }
	}
}
