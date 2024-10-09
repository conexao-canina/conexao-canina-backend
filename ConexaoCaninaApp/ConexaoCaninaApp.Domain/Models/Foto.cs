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
		public int FotoId { get; set; }

		[Required]
		public string CaminhoArquivo { get; set; } // caminho ou o url da imagem
		public string Descricao { get; set; }

		public int Ordem {  get; set; } // para a reordenação

		public int CaoId { get; set; } // chave estrangeira de cao

		public Cao Cao { get; set; }

		public int AlbumId { get; set; }
		public Album Album { get; set; }
	}
}
