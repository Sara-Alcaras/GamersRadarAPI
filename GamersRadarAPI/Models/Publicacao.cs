using System;
using System.ComponentModel.DataAnnotations;

namespace GamersRadarAPI.Models
{
    public class Publicacao
    {
        // [Required] = O campo não pode ser vazio
        // [MaxLength(900)] = Max de caractere permitido
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe sua Descrição")]
        [MaxLength(900)]
        public string Descricao { get; set; }
        public byte[] ImagemAnexo { get; set; }

        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Informe o ID do Perfil")]
        public int PerfilId { get; set; }

    }
}
