using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCenter.Models
{
    public class Galerie
    {
        public int Id { get; set; }

        public String Nom { get; set; }

        public List<Image> ListImages { get; set; } = new List<Image>();
    }
}