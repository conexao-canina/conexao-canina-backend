using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Domain.Models;
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
		bool AlterarIdade(Guid id, int idade);
		CaoDTO GetById(Guid id);
		bool AprovarCao(Guid id);
		bool ReprovarCao(Guid id);
	}

	public class CaoService : ICaoService
	{
		private readonly ICaoRepository _caoRepository;

		public CaoService(ICaoRepository caoRepository)
		{
			_caoRepository = caoRepository;
		}

		public bool AlterarIdade(Guid id, int idade)
		{
			var cao = _caoRepository.GetById(id);
			if (cao == null) return false;

			cao.AlterarIdade(idade);
			_caoRepository.SaveChanges();
			return true;
		}

		public bool AprovarCao(Guid id)
		{
			var cao = _caoRepository.GetById(id);
			if (cao == null) return false;

			cao.Aprovar();
			_caoRepository.SaveChanges();
			return true;
		}

		public IEnumerable<CaoDTO> GetAll()
		{
			var caos = _caoRepository.GetAll();
			if (caos == null) return null;

			return caos.Select(x => new CaoDTO
			{
				CaoId = x.CaoId,
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

		public CaoDTO GetById(Guid id)
		{
			var cao = _caoRepository.GetById(id);
			if (cao == null) return null;

			return new CaoDTO
			{
				CaoId = cao.CaoId,
				CaracteristicasUnicas = cao.CaracteristicasUnicas,
				Idade = cao.Idade,
				Nome = cao.Nome,
				Raca = cao.Raca,
				Cidade = cao.Cidade,
				Descricao = cao.Descricao,
				Estado = cao.Estado,
				Fotos = cao.Fotos.Select(x => new FotoDTO
				{
					FotoId = x.FotoId,
					CaminhoArquivo = x.CaminhoArquivo,
					Descricao = x.Descricao
				}
				).ToList(),
				Genero = cao.Genero,
				Tamanho = cao.Tamanho,
				HistoricosDeSaude = cao.HistoricosDeSaude.Select
				(x => new HistoricoDeSaudeDto
				{
					CondicoesDeSaude = x.CondicoesDeSaude,
					ConsentimentoDono = x.ConsentimentoDono,
					DateExame = x.DataExame,
					HistoricoSaudeId = x.HistoricoSaudeId,
					Vacina = x.Vacina
				})
				
			};
		}

        public bool ReprovarCao(Guid id)
        {
            var cao = _caoRepository.GetById(id);

            if (cao == null) 
				return false;

            cao.Reprovar();
            _caoRepository.SaveChanges();
            return true;
        }
    }
}
