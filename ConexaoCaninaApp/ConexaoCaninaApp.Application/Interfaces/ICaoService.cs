using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface ICaoService
	{
		Task<Cao> AdicionarCao(CaoDto caoDto, ModerarPerfilDto moderarPerfilDto);
		Task AprovarCao(int caoId);
		Task<Cao> ObterPorId(int caoID);
		Task AtualizarCao(EditarCaoDto editarCaoDto, ModerarPerfilDto moderarPerfilDto);
		Task AtualizarInformacoesBasicas(int caoId, AtualizarInformacoesBasicasDto dto);
		Task PublicarCao(int caoId);
		Task<bool> VerificarPerimissaoEdicao(int caoId, int usuarioId);
		Task ExcluirCao(int caoId);
		Task ModerarPerfil(int caoId, ModerarPerfilDto moderarPerfilDto);
	}
}
