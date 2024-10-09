using ConexaoCaninaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Infra.Data.Interfaces
{
	public interface IAlbumRepository
	{
		Task Adicionar(Album album);
		Task Atualizar(Album album);
		Task<Album> ObterPorId(int albumId);
	}
}
