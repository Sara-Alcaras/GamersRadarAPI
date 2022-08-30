using System.ComponentModel.DataAnnotations;

namespace GamersRadarAPI.Models
{
    public class Usuario
    {
        // [Required] = O campo não pode ser vazio
        // [RegularExpression] = Válida se tem ponto no meio e o @
        // [MinLength(6)] = Define um tamanho minímo
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage ="Informe um email válido...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [MinLength(6)]
        public string Senha { get; set; }
      
    }
}
