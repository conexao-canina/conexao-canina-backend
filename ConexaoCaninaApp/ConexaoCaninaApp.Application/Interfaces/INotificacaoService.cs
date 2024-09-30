using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface INotificacaoService
	{
		Task EnviarNotificacaoParaAdministrador(Cao cao);
		Task EnviarNotificacaoParaUsuario(Cao cao);
		Task EnviarNotificacaoDeExclusaoParaUsuario(string emailUsuario, string NomeDoCao);
	}
}
