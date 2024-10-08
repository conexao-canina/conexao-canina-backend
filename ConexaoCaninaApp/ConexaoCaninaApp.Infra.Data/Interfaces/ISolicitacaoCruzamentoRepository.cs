﻿using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface ISolicitacaoCruzamentoRepository
	{
		Task Adicionar(SolicitacaoCruzamento solicitacao);
		Task<SolicitacaoCruzamento> ObterPorId(int id);
		Task Atualizar(SolicitacaoCruzamento solicitacao);
	}
}
