using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCenter.Models;

namespace MediaCenter.Data
{
    public interface IGaleriesRepository
    {
        IQueryable<Galerie> GetAll();
        void Add(IGaleriesRepository galerie);
        Galerie GetById(int id);
        void Remove(Galerie galerie);
        void Update(Galerie galerie);
        bool Exists(int id);
        Galerie GetByIdWithImagesAndtags(int idGalerie);
        void Add(Galerie galerie);
    }
}
