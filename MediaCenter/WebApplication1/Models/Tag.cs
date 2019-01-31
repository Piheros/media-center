using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCenter.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public String Nom { get; set; }

        public int ImageId { get; set; }

        public int Size { get; set; }
    }
}
