using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Image
    {
        public int Id { get; set; }

        public String Nom { get; set; }

        public byte[] Data { get; set; }

        public int GalerieId { get; set; }
    }
}
