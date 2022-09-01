using GamersRadarAPI.Models;
using System.Collections.Generic;

namespace GamersRadarAPI.Interfaces
{
    public interface IPerfilsRepository
    {
        // CRUD
        // Read
        ICollection<Perfil> GetAll();

        Perfil GetById(int id);
        // Create
        Perfil Insert(Perfil perfil);
        // Update
        Perfil Update(int id, Perfil perfil);
    }
}
