﻿using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface IUsuarioRepository : IRepository<Usuario, Guid>
	{
		Usuario GetByEmail(string email);
	}
}
