using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class AtualizarInformacoesBasicasDto
	{
		[Required(ErrorMessage = "Nome é obrigatório")]
		[MaxLength(100, ErrorMessage = "Nome não deve exceder 100 caracteres")]
		public string Nome { get; set; }

		[Range(0, 20, ErrorMessage ="A idade do cachorro deve estar entre 0 até 20")]
		public int Idade { get; set; }

		[Required(ErrorMessage = "Por favor escreva a raça do cão")]
		public string Raca { get; set; }
		[Required(ErrorMessage ="Por favor informe o genero do cão")]
		public int Genero { get; set; } // 1-M, 2-F
		public string CaracteristicasUnicas { get; set; }
	}
}
