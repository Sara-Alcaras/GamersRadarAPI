using GamersRadarAPI.Models;
using System.Collections.Generic;

namespace GamersRadarAPI.Interfaces
{
    public interface IComentariosRepository
    {
        // CRUD
        // Read
        ICollection<Comentarios> GetAll();
        Comentarios GetById(int id);
        // Create
        Comentarios Insert(Comentarios comentario);
        // Update
        Comentarios Update(int id, Comentarios comentario);
        // Delete
        bool Delete(int id);
    }
}
