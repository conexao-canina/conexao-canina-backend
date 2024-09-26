using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Domain.Models
{
    public class Localizacao
    {
        public int LocalizacaoId { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "O país é obrigatório.")]
        public string? Pais {  get; set; }

        // propriedades de navegação
        public ICollection<Proprietario>? Proprietarios { get; set; }
    }
}
