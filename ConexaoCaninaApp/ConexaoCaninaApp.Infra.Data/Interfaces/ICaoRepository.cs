using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface ICaoRepository
	{
		Task Adicionar (Cao cao);
		Task<Cao> ObterPorId (int  id);	
		Task Atualizar (Cao cao);
		Task Remover (Cao cao);
	}
}
