using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaCenter.Data;
using MediaCenter.Models;

namespace MediaCenter.Controllers
{
    public class ImageController : Controller
    {
        private readonly DbContextDemo _context;

        public ImageController(DbContextDemo context)
        {
            _context = context;
        }

        /*
         * INDEX
         */
        public async Task<IActionResult> Index()
        {
            return View(await _context.Image.ToListAsync());
        }

        /*
         * CREATE
         */
        public IActionResult Create(int? id, string nameGalerie)
        {
            ViewBag.id = id;
            ViewBag.nameGalerie = nameGalerie;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nom, Data, GalerieId")] Image image, IList<IFormFile> files)
        {
            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await item.CopyToAsync(memoryStream);
                        image.Data = memoryStream.ToArray();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                await TagRequest.MakeAnalysisRequest(image);

                foreach (Tag tag in image.ListTag)
                {
                    Console.WriteLine(tag.Nom);
                }

                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Image", new { id = image.Id });
            }
            return View(image);
        }

        /*
         * DELETE
         */
        public async Task<IActionResult> Delete(int? id)
        {
            var image = await _context.Image.SingleOrDefaultAsync(m => m.Id == id);
            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Image.SingleOrDefaultAsync(m => m.Id == id);
            _context.Image.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "galerie");
        }

        /*
         * DETAILS
         */
        public async Task<IActionResult> Details(int? id)
        {
            var image = await _context.Image.SingleOrDefaultAsync(i => i.Id == id);
            var ListAllTags = await _context.Tag.ToListAsync();
            return View(image);
        }

        /* 
         * EDIT
         */
        public async Task<IActionResult> Edit(int? id)
        {
            var image = await _context.Image.SingleOrDefaultAsync(m => m.Id == id);
            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Nom, GalerieId")] Image image)
        {
             if (ModelState.IsValid)
            {
                _context.Update(image);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "galerie");
            }
            return View(image);
        }
    }
}
