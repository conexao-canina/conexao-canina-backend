using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class Foto
	{
		private Foto() { }

		public Foto(string caminhoArquivo, string descricao)
		{
			FotoId = Guid.NewGuid();
			CaminhoArquivo = caminhoArquivo;
			Descricao = descricao;
		}

		public Guid FotoId { get; set; }
		public string CaminhoArquivo { get; set; }
		public string Descricao { get; set; }
		
	}
}
