using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Services
{
	public class FotoService : IFotoService
	{
		private readonly IFotoRepository _fotoRepository;
		private readonly IArmazenamentoService _armazenamentoService;
		private readonly ICaoRepository _caoRepository;


		public FotoService(IFotoRepository fotoRepository, IArmazenamentoService armazenamentoService, ICaoRepository caoRepository)
		{
			_fotoRepository = fotoRepository;
			_armazenamentoService = armazenamentoService;
			_caoRepository = caoRepository;
		}

		public async Task<IEnumerable<FotoDto>> UploadFotosAsync(List<IFormFile> arquivos, int caoId)
		{
			var fotosDto = new List<FotoDto>();

			var cao = await _caoRepository.ObterPorId(caoId);

			if (cao == null)
			{
				throw new Exception("Cão não encontrado");
			}
			
			var proprietarioId = cao.ProprietarioId;

			foreach (var arquivo in arquivos)
			{
				
				var caminhoArquivo = await _armazenamentoService.SalvarArquivoAsync(arquivo, proprietarioId);

				var foto = new Foto
				{
					CaminhoArquivo = caminhoArquivo,
					CaoId = caoId,
					Ordem = await _fotoRepository.ObterProximaOrdemAsync(caoId)
				};

				await _fotoRepository.Adicionar(foto);

				fotosDto.Add(new FotoDto
				{
					FotoId = foto.FotoId,
					CaminhoArquivo =caminhoArquivo,
					CaoId = caoId,
					Ordem = foto.Ordem
				});
			}

			return fotosDto;
		}

		public async Task ExcluirFotoAsync(int fotoId)
		{
			var foto = await _fotoRepository.ObterPorId(fotoId);

			if (foto != null)
			{
				await _armazenamentoService.ExcluirArquivoAsync(foto.CaminhoArquivo);
				await _fotoRepository.Remover(foto);
			}
		}

		public async Task ReordenarFotosAsync(IEnumerable<FotoDto> fotos)
		{
			foreach (var fotoDto in fotos)
			{
				var foto = await _fotoRepository.ObterPorId(fotoDto.FotoId);
				if (foto != null)
				{
					foto.Ordem = fotoDto.Ordem;
					await _fotoRepository.Atualizar(foto);
				}
			}
		}
	}
}
