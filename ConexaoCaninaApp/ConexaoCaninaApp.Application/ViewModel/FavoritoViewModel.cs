using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.ViewModel
{
	public class FavoritoViewModel
	{
		public Guid FavoritoId { get; set; }
		public CaoDetalhesViewModel Cao { get; set; }
		public DateTime Data {  get; set; }
	}
}
