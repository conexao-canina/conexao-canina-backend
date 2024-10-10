using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class SolicitacaoCruzamento
	{
		public enum StatusSolicitacao
		{
			Pendente,
			Aceita,
			Rejeitada
		}
		public int SolicitacaoId { get; set; }
		public int UsuarioId { get; set; }
		public int CaoId { get; set; }
		public string Mensagem { get; set; }
		public DateTime DataSolicitacao { get; set; }
		public StatusSolicitacao Status { get; set; }

	}
}
