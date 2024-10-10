using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class RequisitosCruzamentoService : IRequisitosCruzamentoService
	{
		private readonly ICaoRepository _caoRepository;


		public RequisitosCruzamentoService(ICaoRepository caoRepository)
		{
			_caoRepository = caoRepository;
		}


		public async Task DefinirRequisitosCruzamento(int caoId, RequisitosCruzamentoDto dto)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			cao.RequisitosCruzamento = new RequisitosCruzamento
			{
				Temperamento = dto.Temperamento,
				Tamanho = dto.Tamanho,
				CaracteristicasGeneticas = dto.CaracteristicasGeneticas
			};

			await _caoRepository.Atualizar(cao);
		}
		public async Task EditarRequisitosCruzamento(int caoId, RequisitosCruzamentoDto dto)
		{
			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}

			cao.RequisitosCruzamento.Temperamento = dto.Temperamento;
			cao.RequisitosCruzamento.Tamanho = dto.Tamanho;
			cao.RequisitosCruzamento.CaracteristicasGeneticas = dto.CaracteristicasGeneticas;

			await _caoRepository.Atualizar(cao);
		}
	}
}
