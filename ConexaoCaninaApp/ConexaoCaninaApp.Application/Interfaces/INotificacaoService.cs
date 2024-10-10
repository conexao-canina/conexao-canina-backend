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
		Task EnviarNotificacaoParaAdministrador(Cao cao, string observacao);
		Task EnviarNotificacaoParaUsuario(Cao cao);
		Task EnviarNotificacaoDeExclusaoParaUsuario(string emailUsuario, string NomeDoCao, string mensagem);
		Task EnviarNotificacaoSolicitacaoCruzamento(string emailUsuario, string nomeDoCao, string mensagem);
		Task EnviarNotificacaoDeLike(string emailUsuario, string nomeDoCao);
		Task EnviarNotificacaoDeUnlike(string emailUsuario, string nomeDoCao);
	}
}
