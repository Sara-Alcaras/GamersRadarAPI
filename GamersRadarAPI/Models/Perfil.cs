using System.ComponentModel.DataAnnotations;

namespace GamersRadarAPI.Models
{
    public class Perfil
    {
        // [Required] = O campo não pode ser vazio
        // [MaxLength(900)] = Max de caractere permitido
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe sua Biografia")]
        [MaxLength(900)]
        public string Biografia { get; set; }
        public byte[] Foto { get; set; }

        [Required(ErrorMessage = "Informe o jogo de interesse")]
        public string JogosInteresse { get; set; }

        [Required(ErrorMessage = "Informe o ID do Usuário")]
        public int UsuariosId { get; set; }

    }
}
