using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.ViewModel
{
	public class SugestaoViewModel
	{
		public Guid SugestaoId { get; set; }
		public string Descricao { get; set; }
		public DateTime DataEnvio { get; set; }
		public SugestaoStatus Status { get; set; }
		public string Feedback { get; set; }
	}
}
