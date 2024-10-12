using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		[Key]
		public int SolicitacaoId { get; set; }
		public int UsuarioId { get; set; }
		public Usuario Usuario { get; set; }
		public int CaoId { get; set; }
		public Cao Cao { get; set; }
		public string Mensagem { get; set; }
		public DateTime DataSolicitacao { get; set; }
		public StatusSolicitacao Status { get; set; }

	}
}
