using GamersRadarAPI.Models;
using System.Collections.Generic;

namespace GamersRadarAPI.Interfaces
{
    public interface ICategoriaRepository
    {

        // CRUD
        // Read
        ICollection<Categoria> GetAll();
        Categoria GetById(int id);
        // Create
        Categoria Insert(Categoria categoria);
        // Update
        Categoria Update(int id, Categoria categoria);
        // Delete
    }
}
