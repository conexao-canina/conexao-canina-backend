using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class SugestaoDTO
	{
		public Guid SugestaoId { get; set; }
		public string Descricao { get; set; }
		public DateTime DataEnvio { get; set; }
		public SugestaoStatus Status { get; set; }
		public string FeedBack { get; set; }

	}
}
