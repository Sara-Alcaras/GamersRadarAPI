using System.ComponentModel.DataAnnotations;

namespace GamersRadarAPI.Models
{
    public class Categoria
    {
        // [Required] = O campo não pode ser vazio
        // [MaxLength(900)] = Max de caractere permitido
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe uma Categoria")]
        [MaxLength(900)]
        public string TipoCategoria { get; set; }

        [Required(ErrorMessage = "Informe o Id da Publicação")]
        public int PublicacoesId { get; set; }
    }
}
