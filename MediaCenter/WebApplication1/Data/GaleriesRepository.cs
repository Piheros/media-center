using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCenter.Models;

namespace MediaCenter.Data
{
    public class GaleriesRepository : IGaleriesRepository
    {
        private DbContextDemo _context;

        public GaleriesRepository(DbContextDemo context)
        {
            _context = context;
        }

        public IQueryable<Galerie> GetAll()
        {
            throw new NotImplementedException();
        }
        
        public void Add(Galerie galerie)
        { 
            _context.Galerie.Add(galerie);
        }

        public Galerie GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Galerie galerie)
        {
            _context.Galerie.Remove(galerie);
        }

        public void Update(Galerie galerie)
        {
            _context.Galerie.Update(galerie);
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Galerie GetByIdWithImagesAndtags(int idGalerie)
        {
            throw new NotImplementedException();
        }

        public void Add(IGaleriesRepository galerie)
        {
            throw new NotImplementedException();
        }
    }
}
