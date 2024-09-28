using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface IFotoRepository
	{
		Task Adicionar(Foto foto);
		Task<Foto> ObterPorId(int id);
		Task Remover(Foto foto);
		Task Atualizar(Foto foto);
		Task<int> ObterProximaOrdemAsync(int caoId); // vai calcular a proxima ordem
	}
}
