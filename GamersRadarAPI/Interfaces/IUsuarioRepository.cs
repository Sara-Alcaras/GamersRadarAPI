using GamersRadarAPI.Models;
using System.Collections.Generic;

namespace GamersRadarAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        // CRUD
        // Read
        ICollection<Usuario> GetAll();
        Usuario GetById(int id);
        // Create
        Usuario Insert(Usuario usuario);
        // Update
        Usuario Update(int id, Usuario usuario);
   }
}
