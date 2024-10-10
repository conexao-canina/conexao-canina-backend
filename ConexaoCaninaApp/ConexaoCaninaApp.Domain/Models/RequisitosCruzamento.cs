using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
	public class RequisitosCruzamento
	{
		public int RequisitosCruzamentoId { get; set; }
		public string Temperamento { get; set; }

		public string Tamanho { get; set; }
		public string CaracteristicasGeneticas { get; set; }
	}
}
