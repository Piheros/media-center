using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    public class ImageController : Controller
    {
        private readonly DbContextDemo _context;

        public ImageController(DbContextDemo context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Image.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var image = await _context.Image.SingleOrDefaultAsync(i => i.Id == id);
            return View(image);
        }

        public IActionResult Create(int? id, string nameGalerie)
        {
            ViewBag.id = id;
            ViewBag.nameGalerie = nameGalerie;
            return View();
        }

    }
}
