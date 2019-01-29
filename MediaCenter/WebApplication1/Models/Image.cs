using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GalerieId { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nom")]
        public String Nom { get; set; }

        public byte[] Data { get; set; }
    }
}
