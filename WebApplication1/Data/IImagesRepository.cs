using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface IImagesRepository
    {
        IQueryable<Image> GetAll();
        void Add(IImagesRepository image);
        Image GetById(int id);
        void Remove(Image image);
        void Update(Image image);
        bool Exists(int id);
        Image GetByIdWithImagesAndtags(int idImage);
        void Add(Image image);
    }
}