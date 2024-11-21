using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public interface ICaoService
	{
		IEnumerable<CaoDTO> GetAll();	
	}

	public class CaoService : ICaoService
	{
		private readonly ICaoRepository _caoRepository;

		public CaoService(ICaoRepository caoRepository)
		{
			_caoRepository = caoRepository;
		}

		public IEnumerable<CaoDTO> GetAll()
		{
			var caos = _caoRepository.GetAll();
			if (caos == null) return null;

			return caos.Select(x => new CaoDTO
			{
				CaracteristicasUnicas = x.CaracteristicasUnicas,
				Idade = x.Idade,
				Nome = x.Nome,
				Raca = x.Raca,
				Cidade = x.Cidade,
				Descricao = x.Descricao,
				Estado = x.Estado,
				Fotos = x.Fotos.Select(x => new FotoDTO
				{
					FotoId = x.FotoId,
					CaminhoArquivo = x.CaminhoArquivo,
					Descricao = x.Descricao
				}).ToList(),
				Genero = x.Genero,
				Tamanho = x.Tamanho,
			});
		}
	}
}
