using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ImagesRepository : IImagesRepository
    {
        public void Add(IImagesRepository image)
        {
            throw new NotImplementedException();
        }

        public void Add(Image image)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public Image GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Image GetByIdWithImagesAndtags(int idImage)
        {
            throw new NotImplementedException();
        }

        public void Remove(Image image)
        {
            throw new NotImplementedException();
        }

        public void Update(Image image)
        {
            throw new NotImplementedException();
        }
    }
}
