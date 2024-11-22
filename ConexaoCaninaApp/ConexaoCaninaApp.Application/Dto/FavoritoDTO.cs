using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class FavoritoDTO
	{
		public Guid FavoritoId { get; set; }
		public CaoDTO Cao { get; set; }
		public DateTime Data {  get; set; }
	}
}
