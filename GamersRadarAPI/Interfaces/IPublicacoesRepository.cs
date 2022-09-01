using GamersRadarAPI.Models;
using System.Collections.Generic;

namespace GamersRadarAPI.Interfaces
{
    public interface IPublicacoesRepository
    {
        // CRUD
        // Read
        ICollection<Publicacao> GetAll();

        Publicacao GetById(int id);
        // Create
        Publicacao Insert(Publicacao publicacao);
        // Update
        Publicacao Update(int id, Publicacao publicacao);
    }
}
