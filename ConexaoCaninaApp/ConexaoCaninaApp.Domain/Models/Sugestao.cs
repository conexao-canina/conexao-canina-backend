using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public enum SugestaoStatus
	{
		Analise,
		Aprovada,
		Rejeitada
	}

	public class Sugestao
	{
		private Sugestao() { }

		public Sugestao(string descricao, string feedBack)
		{
			SugestaoId = Guid.NewGuid();	
			DataEnvio = DateTime.Now;
			FeedBack = feedBack;
		}

		public Guid SugestaoId { get; set; }
		public string Descricao { get; set; }
		public string FeedBack { get; set; }
		public SugestaoStatus Status { get; set; }
		public DateTime DataEnvio { get; set; }
	
	}
}
