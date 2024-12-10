﻿using ConexaoCaninaApp.Application.Dto;
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
		CaoDTO CriarCao(CaoDTO cao);
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

		public CaoDTO CriarCao(CaoDTO cao)
		{
			var caoDTO = new CaoDTO();
            caoDTO.Nome = cao.Nome;
            caoDTO.Raca = cao.Raca;
            caoDTO.Idade = cao.Idade;
            caoDTO.Descricao = cao.Descricao;
            caoDTO.Genero = cao.Genero;
            caoDTO.Tamanho = cao.Tamanho;
            caoDTO.CaracteristicasUnicas = cao.CaracteristicasUnicas;
            caoDTO.Cidade = cao.Cidade;
            caoDTO.Estado = cao.Estado;
            caoDTO.Fotos = cao.Fotos;
            caoDTO.HistoricosDeSaude = cao.HistoricosDeSaude;
			caoDTO.UserId = cao.UserId;
			caoDTO.CaminhoFoto = cao.CaminhoFoto;

            var caoModel = new Cao
				(caoDTO.Cidade, caoDTO.Estado, caoDTO.Nome, caoDTO.Descricao, caoDTO.Raca, caoDTO.Idade, caoDTO.Tamanho, caoDTO.Genero, caoDTO.CaracteristicasUnicas, 
					caoDTO.Fotos.Select(x => new Foto(x.CaminhoArquivo, x.Descricao)).ToList(), caoDTO.CaminhoFoto);

            _caoRepository.Add(caoModel);
            _caoRepository.SaveChanges();
            return caoDTO;
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
				CaminhoFoto = x.CaminhoFoto,
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
