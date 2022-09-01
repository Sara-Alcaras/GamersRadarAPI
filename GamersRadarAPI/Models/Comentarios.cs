using System;
using System.ComponentModel.DataAnnotations;

namespace GamersRadarAPI.Models
{
    public class Comentarios
    {

        // [Required] = O campo não pode ser vazio
        // [MaxLength(900)] = Max de caractere permitido
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe uma Categoria")]
        [MaxLength(900)]
        public string Comentario { get; set; }
        public DateTime DataComentario { get; set; }

        [Required(ErrorMessage = "Informe o Id do Perfil")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Informe o Id da Publicação")]
        public int PublicacoesId { get; set; }
    }
}
