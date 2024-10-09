using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Domain.Models;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		public async Task<IEnumerable<FotoDto>> UploadFotosAsync(List<IFormFile> arquivos, int caoId, int albumId)
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
					AlbumId = albumId,
					Ordem = await _fotoRepository.ObterProximaOrdemAsync(caoId)
				};

				await _fotoRepository.Adicionar(foto);

				fotosDto.Add(new FotoDto
				{
					FotoId = foto.FotoId,
					CaminhoArquivo =caminhoArquivo,
					CaoId = caoId,
					AlbumId = albumId,
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

		public async Task<IEnumerable<FotoDto>> ObterFotosPorCaoId(int caoId)
		{
			var fotos = await _fotoRepository.ObterFotosPorCaoId(caoId);

			return fotos.Select(f => new FotoDto
			{
				FotoId = f.FotoId,
				CaminhoArquivo = f.CaminhoArquivo,
				Ordem = f.Ordem
			});
		}
		
		public async Task AtualizarOrdemEAdicionarFotosAsync
			(int albumId, List<IFormFile> novasFotos, List<FotoDto> fotosExistentes)
		{
			foreach (var fotoDto in fotosExistentes)
			{
				var foto = await _fotoRepository.ObterPorId(fotoDto.FotoId);

				if (foto != null)
				{
					foto.Ordem = fotoDto.Ordem;

					await _fotoRepository.Atualizar(foto);
				}
			}

			if (novasFotos != null && novasFotos.Any())
			{
				foreach (var novaFoto in novasFotos)
				{
					var caminhoArquivo = await _armazenamentoService.SalvarArquivoAsync(novaFoto, albumId);

					var foto = new Foto
					{
						CaminhoArquivo = caminhoArquivo,
						AlbumId = albumId,
						Ordem = await _fotoRepository.ObterProximaOrdemAsync(albumId)
					};

					await _fotoRepository.Adicionar(foto);
				}
			}
		}

		public async Task RemoverFotoDoAlbumAsync(int fotoId)
		{
			var foto = await _fotoRepository.ObterPorId(fotoId);

			if (foto != null)
			{
				await _armazenamentoService.ExcluirArquivoAsync(foto.CaminhoArquivo);

				await _fotoRepository.Remover(foto);
			}
		}
	}
}
