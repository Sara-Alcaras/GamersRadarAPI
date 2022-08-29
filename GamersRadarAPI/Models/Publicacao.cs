using System;

namespace GamersRadarAPI.Models
{
    public class Publicacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte ImagemAnexo { get; set; }
        public DateTime DataHora { get; set; }

    }
}
