using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
    public class Proprietario
    {
        public int ProprietarioId { get; set; }

        [Required(ErrorMessage = "O nome do proprietário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do proprietário deve ter no máximo 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O e-mail do proprietário é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O telefone do proprietário é obrigatório.")]
        [Phone(ErrorMessage = "O telefone informado não é valido.")]
        public string? Telefone { get; set; }


        // chave estrangeira
        public int LocalizacaoId { get; set; }

        // propriedades de navegação
        public Localizacao Localizacao { get; set; }
        public ICollection<Cao>? Caes { get; set; } // um proprietário pode ter vários cães
    }
}
