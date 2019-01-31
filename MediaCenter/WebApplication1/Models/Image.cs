using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCenter.Models
{
    public class Image
    {
        public int Id { get; set; }

        public String Nom { get; set; }

        public int GalerieId { get; set; }

        public byte[] Data { get; set; }

        public List<Tag> ListTag { get; set; } = new List<Tag>();
    }
}
