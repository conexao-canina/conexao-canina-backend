using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{

    public enum GeneroCao
    {
        Masculino,
        Feminino
    }

    public enum StatusCao
    {
        Pendente,
        Rejeitado,
        Aprovado        
    }

    public enum TamanhoCao
    {
        Pequeno,
        Medio,
        Grande
    }

    public class Cao
    {
        private Cao() { }

        public Cao(string cidade, string estado, string nome,
            string description, string raca, int idade,
            TamanhoCao tamanho, GeneroCao genero, string caracteristicasUnicas,
            List<Foto> fotos, string caminhoFoto)
        {
            CaoId = Guid.NewGuid();
            Cidade = cidade;
            Estado = estado;
            Nome = nome;
            Descricao = description;
            Raca = raca;
            Idade = idade;
            Tamanho = tamanho;
            Genero = genero;
            CaracteristicasUnicas = caracteristicasUnicas;
            Fotos = fotos;
            CaminhoFoto = caminhoFoto;
        }

        public Guid CaoId { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }   
        public int Idade { get; set; }
        public string Raca { get; set; }
        public string CaracteristicasUnicas { get; set; }
        public TamanhoCao Tamanho { get; set; }
        public GeneroCao Genero { get; set; }
        public StatusCao Status { get; set; } = StatusCao.Pendente;
        public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
        public string CaminhoFoto { get; set; }
        public ICollection<HistoricoDeSaude> HistoricosDeSaude { get; set; } = new List<HistoricoDeSaude>();    

        public void AlterarIdade(int idade)
        {
            Idade = idade;
        }
        
        public void AddFoto(Foto foto)
        {
            Fotos.Add(foto);
        }

        public void AddHistoricoDeSaude(HistoricoDeSaude historico)
        {
            HistoricosDeSaude.Add(historico);
        }

        public void Aprovar()
        {
            Status = StatusCao.Aprovado;
        }

        public void Reprovar()
        {
            Status = StatusCao.Rejeitado;
        }
	}
}
