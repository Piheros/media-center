using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Galerie
    {
        public string Nom { get; set; }

        public int Id { get; set; }

        public List<Image> ListImages { get; set; } = new List<Image>();

    }
}
