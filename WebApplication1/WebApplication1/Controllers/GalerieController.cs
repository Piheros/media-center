using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("")]
    public class GalerieController : Controller
    {
        private readonly DbContextDemo _context;
        private IGaleriesRepository _galeries;

        public GalerieController (DbContextDemo context, IGaleriesRepository galeries)
        {
            _context = context;
            _galeries = galeries;
        }

        /*
         * INDEX
         */
        [HttpGet("")]
        public IActionResult Index()
        {
            return View(_context.Galerie);
        }

        /*
         * CREATE
         */
        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create";
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom")] Galerie galerie)
        {
            if (ModelState.IsValid)
            {
                _galeries.Add(galerie);
                _context.SaveChangesAsync();

                //Create folder for images
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "public", galerie.Id.ToString()));

                return RedirectToAction(nameof(Index));
            }
            return View(galerie);
        }

        /*
         * DELETE
         */
        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = "Delete";
            var galerie = await _context.Galerie.SingleOrDefaultAsync(e => e.Id == id);
            return View(galerie);
        }

        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind("Id,Nom")] Galerie galerie)
        {
            if (ModelState.IsValid)
            {
                _galeries.Remove(galerie);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                /**
                    var galerie = await _context.Galerie.SingleOrDefaultAsync(e => e.Id == id);
                    _context.Galerie.Remove(galerie);
                    await _context.SaveChangesAsync();

                    //Create folder for images
                    //Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "public", galerie.Id.ToString()));

                    return RedirectToAction(nameof(Index));
               **/
            }
            return View(galerie);
        }

        /*
         * EDIT
         */
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = "Edit";
            var galerie = await _context.Galerie.SingleOrDefaultAsync(e => e.Id == id);
            return View(galerie);
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Nom")] Galerie galerie)
        {
            if (ModelState.IsValid)
            {
                _galeries.Update(galerie);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
             
            }
            return View(galerie);
        }

    }
}
