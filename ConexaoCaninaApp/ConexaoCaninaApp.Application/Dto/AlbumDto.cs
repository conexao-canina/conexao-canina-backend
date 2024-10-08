﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class AlbumDto
	{
		public int AlbumId { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }

		public int ProprietarioId { get; set; }

		public string Privacidade { get; set; }
		public List<int> UsuariosPermitidosIds { get; set; } = new List<int>(); 

	}
}
