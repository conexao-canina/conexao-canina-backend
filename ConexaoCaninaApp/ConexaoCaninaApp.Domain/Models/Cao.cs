using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{

    public enum StatusCao
    {
        Pendente,
        Aprovado,
        Publicado
    }

    public enum TamanhoCao
    {
        Pequeno,
        Medio,
        Grande
    }

    public class Cao
    {
        public int CaoId { get; set; }

        [Required(ErrorMessage = "O nome do cão é obrigatório.")]
        [StringLength(30,ErrorMessage = "O nome do cão deve ter no máximo 30 caracteres.")]
        public string? Nome { get; set; }

        [StringLength(500, ErrorMessage = "A descrição do cão deve ter no máximo 500 caracteres.")]
        public string? Descricao { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A idade do cão deve ser maior que 0.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "A raça do cão é obrigatória.")]
        public string? Raca { get; set; }

        [Required(ErrorMessage = "O gênero do cão é obrigatório.")]
        public int Genero { get; set; } // 1-M | 2-F

        [Required(ErrorMessage = "O tamanho do cão é obrigatório")]
        public TamanhoCao Tamanho { get; set; }

        public string? CaracteristicasUnicas { get; set; }

        public StatusCao Status { get; set; } = StatusCao.Pendente;

        public ICollection<Foto> Fotos { get; set; } // navegação para fotos

		// chave estrangeira
		public int ProprietarioId { get; set; }

        // propriedade de navegação
        public Proprietario? Proprietario { get; set; }

        public ICollection<HistoricoSaude> HistoricosDeSaude { get; set; }
    }
}
