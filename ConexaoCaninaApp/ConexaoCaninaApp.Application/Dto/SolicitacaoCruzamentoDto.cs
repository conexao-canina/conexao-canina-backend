using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class SolicitacaoCruzamentoDto
	{
		public int UsuarioId { get; set; }
		public Cao Cao { get; set; }
		public int CaoId { get; set; }
		public string Mensagem { get; set; }
	}
}
