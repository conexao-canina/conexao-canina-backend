using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface  ISugestaoRepository
	{
		Task AdicionarAsync(Sugestao sugestao);
		Task<Sugestao> ObterPorIdAsync(int sugestaoId);
		Task<List<Sugestao>> ObterSugestoesPorUsuarioAsync(int usuarioId);
		Task AtualizarAsync(Sugestao sugestao);
	}
}
