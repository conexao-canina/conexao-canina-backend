using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexaoCaninaApp.Application.Dto;
using ConexaoCaninaApp.Domain.Models;

namespace ConexaoCaninaApp.Application.Requests
{
    public class CriarCaoRequest
    {
        public string Nome { get; set; }
        public string Raca { get; set; }
        public int Idade { get; set; }
        public string Descricao { get; set; }
        public GeneroCao Genero { get; set; }
        public TamanhoCao Tamanho { get; set; }
        public string CaracteristicasUnicas { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public List<FotoDTO> Fotos { get; set; } = new List<FotoDTO>();
        public IEnumerable<HistoricoDeSaudeDto> HistoricosDeSaude { get; set; }
    }
}
