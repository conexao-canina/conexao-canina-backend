using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class AdicionarCaoDTO
	{
		public string Cidade { get; set; }
		public string Estado { get; set; }
		public string? Nome { get; set; } 
		public string? Descricao { get; set; }
		public int Idade { get; set; }
		public string? Raca { get; set; }
		public TamanhoCao Tamanho { get; set; }
		public GeneroCao Genero { get; set; }
		public string? CaracteristicasUnicas { get; set; }
		public ICollection<FotoDTO> Fotos { get; set; }		
	}
}
