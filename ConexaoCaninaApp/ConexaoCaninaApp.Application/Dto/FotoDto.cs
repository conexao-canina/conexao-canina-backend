using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class FotoDto
	{
		public int FotoId { get; set; }
		public string CaminhoArquivo { get; set; }
		public int Ordem { get; set; }
		public int CaoId { get; set; }
	}
}
