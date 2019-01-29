using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DbContextDemo : DbContext
    {   
        public DbContextDemo(DbContextOptions<DbContextDemo> options) : base(options) { }

        public DbSet<Galerie> Galerie {get; set;}

        public DbSet<Image> Image {get; set;}
    }
}
