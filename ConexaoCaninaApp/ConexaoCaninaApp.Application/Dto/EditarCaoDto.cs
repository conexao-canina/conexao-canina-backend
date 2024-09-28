using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class EditarCaoDto
	{
		public int CaoId { get; set; }
		public int Idade { get; set; }
		public string Descricao { get; set; }
		public string CaracteristicasUnicas { get; set; }
	}
}